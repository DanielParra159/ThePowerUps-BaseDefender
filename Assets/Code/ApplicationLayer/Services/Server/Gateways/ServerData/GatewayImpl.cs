using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos;
using Domain.Services.Serializer;
using Domain.Services.Server;
using UnityEngine.Assertions;

namespace ApplicationLayer.Services.Server.Gateways.ServerData
{
    public abstract class GatewayImpl : Gateway, DataPreLoaderService
    {
        private Dictionary<string, string> _rawData;
        private Dictionary<Type, string> _typeToKey;
        private Dictionary<Type, Dto> _parsedData;
        private readonly Dictionary<string, string> _dirtyData;

        private readonly SerializerService _serializerService;
        private readonly GetDataService _getDataService;
        private readonly SetDataService _setDataService;

        private bool _isInitialized;

        protected GatewayImpl(SerializerService serializerService,
                              GetDataService getDataService,
                              SetDataService setDataService)
        {
            _serializerService = serializerService;
            _getDataService = getDataService;
            _setDataService = setDataService;
            _dirtyData = new Dictionary<string, string>();
        }

        protected abstract void InitializeTypeToKey(out Dictionary<Type, string> typeToKey);

        public async Task PreLoad()
        {
            InitializeTypeToKey(out _typeToKey);

            var optional = await _getDataService.Get(new List<string>());

            optional
                .IfPresent(result =>
                {
                    _rawData = result.Data;
                    _parsedData = new Dictionary<Type, Dto>(_rawData.Count);
                    _isInitialized = true;
                })
                .OrElseThrow(new Exception("Error initializing gateway data"));
        }

        public T Get<T>() where T : Dto
        {
            Assert.IsTrue(_isInitialized, "Gateway is not initialized");
            
            var type = typeof(T);

            if (_parsedData.TryGetValue(type, out var result))
            {
                return (T) result;
            }

            var key = _typeToKey[type];
            var data = _rawData[key];

            var dto = _serializerService.Deserialize<T>(data);
            _parsedData.Add(type, dto);

            return dto;
        }

        public bool Contains<T>() where T : Dto
        {
            Assert.IsTrue(_isInitialized, "Gateway is not initialized");

            var type = typeof(T);
            var key = _typeToKey[type];
            return _rawData.ContainsKey(key);
        }

        public void Set<T>(T data) where T : Dto
        {
            Assert.IsTrue(_isInitialized, "Gateway is not initialized");
            
            var type = typeof(T);
            var key = _typeToKey[type];
            if (_dirtyData.ContainsKey(key))
            {
                throw new Exception($"This key {type} is already dirty");
            }

            var serializedData = _serializerService.Serialize(data);
            SetRawData(key, serializedData);
            SetParsedData(data, type);
            _dirtyData.Add(key, serializedData);
        }

        private void SetRawData(string key, string serializedData)
        {
            if (_rawData.ContainsKey(key))
            {
                _rawData[key] = serializedData;
            }
            else
            {
                _rawData.Add(key, serializedData);
            }
        }

        private void SetParsedData<T>(T data, Type type) where T : Dto
        {
            if (!_parsedData.ContainsKey(type))
            {
                _parsedData.Add(type, data);
            }
            else
            {
                _parsedData[type] = data;
            }
        }

        public Task Save()
        {
            Assert.IsTrue(_isInitialized, "Gateway is not initialized");
            
            var task = _setDataService.Set(_dirtyData);
            _dirtyData.Clear();
            return task;
        }

      
    }
}

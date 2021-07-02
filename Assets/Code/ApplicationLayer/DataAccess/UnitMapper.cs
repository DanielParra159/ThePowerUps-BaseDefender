using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApplicationLayer.Services.Server.Dtos.Server;
using ApplicationLayer.Services.Server.Dtos.User;
using Code.SharedTypes.Units;
using Domain.Entities;
using Domain.Services.Serializer;
using PlayFab.ServerModels;
using UnityEngine;
using CatalogItem = PlayFab.ClientModels.CatalogItem;

namespace ApplicationLayer.DataAccess
{
    public class UnitMapper
    {
        private static SerializerService _serializerService;

        public UnitMapper(SerializerService serializerService)
        {
            _serializerService = serializerService;
        }

        public static CatalogItemDto ParseCatalogItem<T>(CatalogItem item)
        {
            return new CatalogItemDto(
                item.ItemId,
                item.DisplayName,
                _serializerService.Deserialize<T>(item.CustomData)
            );
        }

        public static InventoryItemDto ParseInventoryItem<T>(GrantedItemInstance item)
        {
            return new InventoryItemDto(item.ItemId,
                item.ItemInstanceId,
                _serializerService.Deserialize<T>(MyDictionaryToJson(item.CustomData)));
        }

        public static InventoryItemDto ParseInventoryItem<T>(ItemInstance item)
        {
            return new InventoryItemDto(item.ItemId,
                item.ItemInstanceId,
                _serializerService.Deserialize<T>(MyDictionaryToJson(item.CustomData)));
        }

        private static string MyDictionaryToJson(Dictionary<string, string> dict)
        {
            var entries = dict.Select(d =>
                $"\"{d.Key}\": {d.Value}");
            var myDictionaryToJson = "{" + string.Join(",", entries) + "}";
            return myDictionaryToJson;
        }

        public static Unit ParseUnits(CatalogItemDto unitDto)
        {
            return ParseUnit(unitDto);
        }

        public static Unit ParseUnit(CatalogItemDto unitDto)
        {
            Debug.Log($"Parsing unit: {unitDto.ID}");
            var unitCustomData = unitDto.GetCustomData<UnitCustomData>();
            var unit = new Unit(unitDto.ID,
                unitDto.DisplayName,
                unitCustomData.Attributes);
            return unit;
        }

        public static UserUnit InventoryItemToUserUnit(Unit unit, InventoryItemDto userUnit)
        {
            return new UserUnit(unit, userUnit.InstanceId, userUnit.GetCustomData<UnitState>());
        }
        
        public static Dictionary<string, string> ParseFieldsToDictionary<T>(T unitState)
        {
            var type = unitState.GetType();
            var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            return fieldInfos.ToDictionary(field => field.Name,
                field => field.GetValue(unitState).ToString());
        }
    }
}
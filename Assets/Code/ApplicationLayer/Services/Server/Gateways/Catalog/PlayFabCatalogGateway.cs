using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.DataAccess;
using ApplicationLayer.Services.Server.Dtos.Server;
using Domain.Services.Server;
using PlayFab.ClientModels;
using UnityEngine.Assertions;

namespace ApplicationLayer.Services.Server.Gateways.Catalog
{
    public class PlayFabCatalogGateway : CatalogGateway, CatalogPreLoaderService
    {
        private readonly CatalogService _catalogService;

        private readonly Dictionary<string, List<CatalogItemDto>> _items;

        public PlayFabCatalogGateway(CatalogService catalogService)
        {
            _catalogService = catalogService;
            _items = new Dictionary<string, List<CatalogItemDto>>();
        }

        public async Task PreLoad<T>(string catalogId)
        {
            await GetItemsFromServer<T>(catalogId);
        }
        
        public IReadOnlyList<CatalogItemDto> GetItems(string catalogId)
        {
            Assert.IsTrue(_items.ContainsKey(catalogId), "You need to call to PreLoad first");

            return GetCachedItems(catalogId);
        }

        private List<CatalogItemDto> GetCachedItems(string catalogId)
        {
            return _items[catalogId];
        }

        private async Task GetItemsFromServer<T>(string catalogId)
        {
            var optionalItems = await _catalogService.GetItems(catalogId);
            optionalItems
                .IfPresent(items => { ParseCatalogItems<T>(catalogId, items); })
                .OrElseThrow(new Exception());
        }

        private void ParseCatalogItems<T>(string catalogId, IEnumerable<CatalogItem> items)
        {
            var result = new List<CatalogItemDto>(items
                .Select(UnitMapper.ParseCatalogItem<T>));
            _items.Add(catalogId, result);
        }

        
    }
}
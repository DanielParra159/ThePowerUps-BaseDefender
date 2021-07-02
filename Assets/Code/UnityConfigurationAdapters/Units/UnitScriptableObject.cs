using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Services.Server.Dtos.Server;
using NaughtyAttributes;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Units
{
    [CreateAssetMenu(menuName = "Base Defender/Units/New Unit", fileName = "UnitConfiguration", order = 0)]
    public class UnitScriptableObject : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField] private UnitCustomData customData;

#if UNITY_EDITOR && ENABLE_PLAYFABADMIN_API
        [Button()]
        public void LoadFromServer()
        {
            var request = new PlayFab.AdminModels.GetCatalogItemsRequest
            {
                CatalogVersion = "Units"
            };
            PlayFab.PlayFabAdminAPI.GetCatalogItems(
                request,
                result =>
                {
                    var catalogItem = result.Catalog
                        .First(item => item.ItemId.Equals(id));
                    displayName = catalogItem.DisplayName;
                    var unitCustomData = JsonUtility.FromJson<UnitCustomData>(catalogItem.CustomData);
                    customData = unitCustomData;

                    UnityEditor.EditorUtility.SetDirty(this);
                    UnityEditor.AssetDatabase.SaveAssets();
                    UnityEditor.AssetDatabase.Refresh();
                },
                error => throw new Exception(error.ErrorMessage));
        }

        [Button()]
        public void SaveOnServer()
        {
            var request = new PlayFab.AdminModels.UpdateCatalogItemsRequest
            {
                Catalog = new List<PlayFab.AdminModels.CatalogItem>
                {
                    new PlayFab.AdminModels.CatalogItem
                    {
                        ItemId = id,
                        DisplayName = displayName,
                        CustomData = JsonUtility.ToJson(customData)
                    }
                },
                CatalogVersion = "Units",
            };
            PlayFab.PlayFabAdminAPI
                .UpdateCatalogItems(request,
                    result => { Debug.Log("Saved"); },
                    error => throw new Exception(error.ErrorMessage));
        }

#endif
    }
}
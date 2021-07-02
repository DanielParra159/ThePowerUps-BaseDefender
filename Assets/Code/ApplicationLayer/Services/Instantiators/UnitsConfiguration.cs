using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemUtilities;
using Domain.Services.Server;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace ApplicationLayer.Services.Instantiators
{
    [CreateAssetMenu(menuName = "BaseDefender/Units/Create UnitsConfiguration", fileName = "Units configuration",
        order = 0)]
    public class UnitsConfiguration : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject[] unitPrefabs;
        private Dictionary<int, AssetReferenceGameObject> _unitIdToUnitPrefab;

        public AssetReferenceGameObject[] UnitPrefabs => unitPrefabs;

        public void CreateMapper()
        {
            _unitIdToUnitPrefab = new Dictionary<int, AssetReferenceGameObject>(unitPrefabs.Length);

            foreach (var unitPrefab in unitPrefabs)
            {
                _unitIdToUnitPrefab.Add(unitPrefab.Asset.name.GetHashCode(), unitPrefab);
            }
        }


        public AssetReference GetUnit(string unitId)
        {
            Assert.IsTrue(_unitIdToUnitPrefab.ContainsKey(unitId.GetHashCode()), $"Unit {unitId} does not exit");

            return _unitIdToUnitPrefab[unitId.GetHashCode()];
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class WoodFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Wood woodPrefab;

        public override GameObject CreateTile(TileLevelData tileLevelData)
        {
            var wood = GetWoodInPool();

            var iWood = wood.GetComponent<IWood>();
            var iTile = wood.GetComponent<ITile>();

            iTile.SetPosition(tileLevelData.coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Wood);

            iWood.SetShield(2);

            return wood;
        }

        private GameObject GetWoodInPool()
        {
            var wood = _createdWoodList.FirstOrDefault(x => !x.activeInHierarchy);
            if (wood != null)
            {
                wood.SetActive(true);
                return wood;
            }

            wood = _diContainer.InstantiatePrefab(woodPrefab);
            _createdWoodList.Add(wood);

            return wood;
        }

        #region Parameters

        private readonly List<GameObject> _createdWoodList = new();
        [Inject] private DiContainer _diContainer;

        #endregion
    }
}
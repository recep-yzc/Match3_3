using System.Collections.Generic;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.TileSystem.Elements.Gem.Scripts
{
    [DefaultExecutionOrder(-2)]
    public class GemFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        #region UnityActions

        protected override void Start()
        {
            TileConstants.FactoryList.TryAdd(TileId.Gem, this);
        }

        #endregion

        public override GameObject Create(TileData tileData)
        {
            var tilePropertyGem = (GemElementData)tileData.elementData;
            var gemGameObject = GetGameObjectInPool(ref _createdGemList, gemPrefab.gameObject);

            var iTile = gemGameObject.GetComponent<ITile>();
            var iGem = gemGameObject.GetComponent<IGem>();

            iTile.SetPosition(tileData.coordinate);
            iTile.SetParent(transform);
            iTile.TileId = TileId.Gem;

            var gemElementDataSo = _gemController.GetGemDataSo(tilePropertyGem.gemId);

            iGem.SetGemId(gemElementDataSo.data.gemId);
            iGem.SetSprite(gemElementDataSo.data.icon);

            return gemGameObject;
        }

        #region Parameters

        private List<GameObject> _createdGemList = new();
        [Inject] private GemController _gemController;

        #endregion
    }
}
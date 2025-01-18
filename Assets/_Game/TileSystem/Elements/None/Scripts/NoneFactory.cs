using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.TileSystem.Elements.None.Scripts
{
    [DefaultExecutionOrder(-2)]
    public class NoneFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private NoneElementDataSo noneDataSo;

        protected override void Start()
        {
            TileConstants.FactoryList.TryAdd(TileId.None, this);
        }

        public override GameObject Create(TileData tileData)
        {
            return null;
        }
    }
}
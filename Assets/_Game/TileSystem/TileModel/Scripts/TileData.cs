using System;
using _Game.MathSystem.VectorModel.Scripts;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.Fall.Scripts;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    [Serializable]
    public class TileData
    {
        public TileData(Vector2 coordinate, GameObject gameObject)
        {
            SetIsEmpty(false);
            SetCoordinate(coordinate);
            SetGameObject(gameObject);

            NeighborTileData = new TileData[4];
        }

        public void SetNeighborTileData(TileData[] neighborTileData)
        {
            Array.Copy(neighborTileData, NeighborTileData, NeighborTileData.Length);
        }

        public void SetCoordinate(Vector2 coordinate)
        {
            Coordinate = coordinate;
        }

        public void SetIsEmpty(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        public void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public bool IsEmpty { get; private set; }
        public Vector2 Coordinate { get; private set; }
        public GameObject GameObject { get; private set; }
        public Vector2 BottomLeft => Coordinate - VectorHelper.Half;
        public Vector2 TopRight => Coordinate + VectorHelper.Half;
        public TileData[] NeighborTileData { get; private set; }

        #region Tile

        public ITile Tile => _tile ??= GameObject.GetComponent<ITile>();
        private ITile _tile;
        public IGem Gem => _gem ??= GameObject.GetComponent<IGem>();
        private IGem _gem;
        public IWood Wood => _wood ??= GameObject.GetComponent<IWood>();
        private IWood _wood;

        #endregion

        #region Ability

        public IBlast Blast => _blast ??= GameObject.GetComponent<IBlast>();
        private IBlast _blast;

        public IFall Fall => _fall ??= GameObject.GetComponent<IFall>();
        private IFall _fall;

        public IShake Shake => _shake ??= GameObject.GetComponent<IShake>();
        private IShake _shake;

        public IScaleUpDown ScaleUpDown => _scaleUpDown ??= GameObject.GetComponent<IScaleUpDown>();
        private IScaleUpDown _scaleUpDown;

        #endregion
    }
}
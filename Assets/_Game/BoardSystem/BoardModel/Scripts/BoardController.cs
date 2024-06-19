using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.GridSystem.GridModel.Scripts;
using _Game.TileSystem.DirectionModel.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardController : MonoBehaviour
    {
        #region First

        private void Start()
        {
            Application.targetFrameRate = 60;
            FetchGridDataSo();
            CreateTile();
        }

        #endregion

        private void CreateTile()
        {
            var gridList = _gridDataSo.playableGridList;
            var tempList = new List<TileData>(gridList.Count);

            foreach (var coordinate in gridList)
            {
                var tile = _gemFactory.CreateTile(coordinate);
                tempList.Add(new TileData(coordinate, tile));
            }

            BoardConstants.TileData.Clear();
            BoardConstants.TileData.AddRange(tempList);

            Parallel.ForEach(tempList, tileData =>
            {
                var neighborTileList = GetNeighborTiles(tileData.Coordinate);
                tileData.SetNeighborTileData(neighborTileList);
            });
        }

        private TileData[] GetNeighborTiles(Vector2 coordinate)
        {
            var array = DirectionHelper.GetAsArray();
            var tileData = new TileData[array.Length];

            for (var i = 0; i < array.Length; i++)
            {
                var direction = array[i];
                var newCoordinate = coordinate + direction.ToVector();

                tileData[i] = BoardHelper.GetTileDataByCoordinate(BoardConstants.TileData, newCoordinate);
            }

            return tileData;
        }

        private void FetchGridDataSo()
        {
            _gridDataSo = _gridLevelDataSo.GetCurrentGridDataSo();
        }

        #region Private

        #region Level

        [Inject] private GridLevelDataSo _gridLevelDataSo;
        private GridDataSo _gridDataSo;

        #endregion

        #region Ability

        [Inject] private GemFactory _gemFactory;
        [Inject] private WoodFactory _woodFactory;

        #endregion

        #endregion

        public void TryCreate(List<TileData> tileData)
        {
        }
    }
}
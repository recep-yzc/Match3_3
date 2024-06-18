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
        [Header("References")] [SerializeField]
        private GemFactory gemFactory;

        [SerializeField] private WoodFactory woodFactory;

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

            for (var i = 0; i < gridList.Count; i++)
            {
                var coordinate = gridList[i];
                var tile = i <= (int)(gridList.Count * 0.5f)
                    ? woodFactory.CreateTile(coordinate)
                    : gemFactory.CreateTile(coordinate);
                var bottomLeft = coordinate - _halfGridSize;
                var topRight = coordinate + _halfGridSize;

                tempList.Add(new TileData(coordinate, tile, bottomLeft, topRight));
            }

            BoardConstants.TileData.Clear();
            BoardConstants.TileData.AddRange(tempList);

            Parallel.ForEach(tempList, tileData =>
            {
                var neighborTileList = GetNeighborTiles(tileData.Coordinate);
                tileData.SetNeighborTiles(neighborTileList);
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

                var result = BoardHelper.GetTileDataByCoordinate(BoardConstants.TileData, newCoordinate);
                tileData[i] = result;
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

        private readonly Vector2 _halfGridSize = new(0.5f, 0.5f);

        #endregion
    }
}
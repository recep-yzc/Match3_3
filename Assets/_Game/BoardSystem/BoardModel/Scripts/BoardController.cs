using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.DictionarySystem.DictionaryModel.Scripts;
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
            ClearTile();
            CreateTile();
            FetchNeighbor();
        }
        
        #endregion

        private void ClearTile()
        {
            BoardConstants.TileData.Clear();
            BoardConstants.VerticalTileData.Clear();
            BoardConstants.HorizontalTileData.Clear();
        }

        private void CreateTile()
        {
            var gridList = _gridDataSo.playableGridList;

            foreach (var coordinate in gridList)
            { 
                var tile = _gemFactory.CreateTile(coordinate);
                var tileData = new TileData(coordinate, tile);

                DictionaryHelper.AddTileDataToDictionary(BoardConstants.HorizontalTileData, coordinate.x, tileData);
                DictionaryHelper.AddTileDataToDictionary(BoardConstants.VerticalTileData, coordinate.y, tileData);
                BoardConstants.TileData.Add(tileData);
            }
            
            DictionaryHelper.OrderByHorizontal(BoardConstants.HorizontalTileData);
            DictionaryHelper.OrderByVertical(BoardConstants.VerticalTileData);
        }

        private void FetchNeighbor()
        {
            Parallel.ForEach(BoardConstants.TileData, tileData =>
            {
                var neighborTileList = GetNeighborTiles(tileData.Coordinate);
                tileData.SetNeighborTileData(neighborTileList);
            });
        }
        
        public void TryCreate(List<TileData> tileData)
        {
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
    }
}
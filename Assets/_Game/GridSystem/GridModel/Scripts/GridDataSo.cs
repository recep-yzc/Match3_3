using System;
using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace _Game.GridSystem.GridModel.Scripts
{
    [CreateAssetMenu(fileName = "GridDataSo", menuName = "Game/Grid/Data")]
    public class GridDataSo : ScriptableObject
    {
        [PropertyOrder(0)] public List<TileLevelData> tileLevelData = new();

        [ShowInInspector]
        [MinValue(2)]
        [MaxValue(100)]
        [PropertyOrder(0)]
        public int Rows
        {
            get => row;
            set => row = value;
        }

        [ShowInInspector]
        [MinValue(2)]
        [MaxValue(100)]
        [PropertyOrder(1)]
        public int Columns
        {
            get => column;
            set => column = value;
        }

        [Button]
        [PropertyOrder(4)]
        public void CreateTileLevelData()
        {
            tileLevelData = new();
            var halfOfRows = Rows * 0.5f;
            var halfOfColumns = Columns * 0.5f;
            var offset = new Vector2(halfOfRows, halfOfColumns) - Vector2.one * 0.5f;

            for (var x = 0; x < Rows; x++)
            for (var y = 0; y < Columns; y++)
                tileLevelData.Add(new TileLevelData()
                {
                    coordinate = new Vector2(x, y) - offset
                });
        }

        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        #region Public

        [HideInInspector] public int row;
        [HideInInspector] public int column;

        #endregion
    }
}
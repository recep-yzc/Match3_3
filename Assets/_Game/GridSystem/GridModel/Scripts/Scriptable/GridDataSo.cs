using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Game.GridSystem.GridModel.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "GridDataSo", menuName = "Game/Grid/Data")]
    public class GridDataSo : ScriptableObject
    {
        [ShowInInspector, MinValue(2), MaxValue(100), PropertyOrder(0)]
        public int Rows
        {
            get => row;
            set
            {
                row = value;
                UpdateGridList();
            }
        }

        [ShowInInspector, MinValue(2), MaxValue(100), PropertyOrder(1)]
        public int Columns
        {
            get => column;
            set
            {
                column = value;
                UpdateGridList();
            }
        }

        [PropertyOrder(2)] public List<Vector2> allGridList;
        [PropertyOrder(3)] public List<Vector2> playableGridList;

        #region Public

        [HideInInspector] public int row;
        [HideInInspector] public int column;

        #endregion

        private void UpdateGridList()
        {
            allGridList = new();

            var halfOfRows = Rows * 0.5f;
            var halfOfColumns = Columns * 0.5f;
            var offset = new Vector2(halfOfRows, halfOfColumns) - Vector2.one * 0.5f;

            for (var x = 0; x < Rows; x++)
            {
                for (var y = 0; y < Columns; y++)
                {
                    allGridList.Add(new Vector2(x, y) - offset);
                }
            }
        }

        [Button, PropertyOrder(4)]
        public void ResetPlayableGridList()
        {
            playableGridList = new List<Vector2>(allGridList);
        }

        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}
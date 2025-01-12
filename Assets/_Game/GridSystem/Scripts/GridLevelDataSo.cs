using System.Collections.Generic;
using _Game.LevelSystem.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.GridSystem.Scripts
{
    [CreateAssetMenu(fileName = "GridLevelDataSo", menuName = "Game/Grid/Level/Data")]
    public class GridLevelDataSo : ScriptableObjectInstaller<GridLevelDataSo>
    {
        public List<GridDataSo> gridDataSo;

        public GridDataSo GetCurrentGridDataSo()
        {
            var repeatLevel = (int)Mathf.Repeat(LevelPrefs.CurrentLevel, gridDataSo.Count);
            return gridDataSo[repeatLevel];
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
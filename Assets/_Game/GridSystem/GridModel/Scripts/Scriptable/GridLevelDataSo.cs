using System.Collections.Generic;
using _Game.GridSystem.GridModel.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace _Game.GridSystem.GridModel.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "GridLevelDataSo", menuName = "Game/Grid/Level/Data")]
    public class GridLevelDataSo : ScriptableObjectInstaller<GridLevelDataSo>
    {
        public List<GridDataSo> gridDataSo;

        public GridDataSo GetCurrentGridDataSo()
        {
            var repeatLevel = (int)Mathf.Repeat(GridPrefs.CurrentLevel, gridDataSo.Count);
            return gridDataSo[repeatLevel];
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
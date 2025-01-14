using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.Level.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "LevelReferenceDataSo", menuName = "Game/Level/Reference/Data")]
    public class LevelReferenceDataSo : ScriptableObjectInstaller<LevelReferenceDataSo>
    {
        public List<LevelDataSo> levelDataSoList;

        public LevelDataSo GetCurrentLevelDataSo()
        {
            var repeatLevel = (int)Mathf.Repeat(LevelPrefs.CurrentLevel, levelDataSoList.Count);
            return levelDataSoList[repeatLevel];
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
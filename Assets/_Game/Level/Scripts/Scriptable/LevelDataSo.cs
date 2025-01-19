using System.Collections.Generic;
using _Game.Core.Elements.Gem.Scripts;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Game.Level.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "LevelDataSo", menuName = "Game/Level/Data")]
    public class LevelDataSo : ScriptableObject
    {
        [HideInInspector] public List<LevelGridData> levelGridDataList;

        [MinValue(2)] [MaxValue(100)] [PropertyOrder(-99)]
        public int rows;

        [MinValue(2)] [MaxValue(100)] [PropertyOrder(-98)]
        public int columns;

#if UNITY_EDITOR
        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        [Header("Generate")] [PropertyOrder(-97)]
        public GenerateType generateType;

        [ShowIf("generateType", GenerateType.WithSelectedRandomGem)] [SerializeField] [PropertyOrder(-96)]
        private List<GemId> gemIdList;

        [Button]
        [PropertyOrder(-96)]
        public void Generate()
        {
            LevelGenerator.Instance?.GenerateLevelDataSo(this, generateType, gemIdList);
        }
#endif
    }
}
using UnityEngine;

namespace _Game.GridSystem.GridModel.Scripts.Utilities
{
    public static class GridPrefs
    {
        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt(nameof(CurrentLevel), 0);
            set => PlayerPrefs.SetInt(nameof(CurrentLevel), value);
        }
    }
}
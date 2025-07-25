using UnityEngine;

namespace _Game.Level.Scripts
{
    public static class LevelPrefs
    {
        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt(nameof(CurrentLevel), 0);
            set => PlayerPrefs.SetInt(nameof(CurrentLevel), value);
        }
    }
}
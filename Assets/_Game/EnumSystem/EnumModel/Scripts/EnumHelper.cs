using System;

namespace _Game.EnumSystem.EnumModel.Scripts
{
    public static class EnumHelper
    {
        public static int GetEnumLength<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}
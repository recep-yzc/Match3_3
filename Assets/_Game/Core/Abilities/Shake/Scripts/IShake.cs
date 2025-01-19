using Cysharp.Threading.Tasks;

namespace _Game.Core.Abilities.Shake.Scripts
{
    public interface IShake
    {
        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo);
    }
}
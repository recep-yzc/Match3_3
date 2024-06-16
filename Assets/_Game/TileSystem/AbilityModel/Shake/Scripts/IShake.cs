using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.AbilityModel.Shake.Scripts
{
    public interface IShake
    {
        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo);
    }
}
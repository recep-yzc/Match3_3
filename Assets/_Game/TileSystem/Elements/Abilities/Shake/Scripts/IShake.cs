using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.Elements.Abilities.Shake.Scripts
{
    public interface IShake
    {
        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo);
    }
}
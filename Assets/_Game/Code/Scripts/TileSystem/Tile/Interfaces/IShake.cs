using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Code.Scripts.TileSystem.Tile.Interfaces
{
    public interface IShake
    {
        public UniTaskVoid Shake(float duration, float force, AnimationCurve animationCurve);
    }
}
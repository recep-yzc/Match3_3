using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.Elements.Abilities.Fall.Scripts
{
    public interface IFall
    {
        public UniTask FallAsync(Vector2 position, FallDataSo fallDataSo);
    }
}
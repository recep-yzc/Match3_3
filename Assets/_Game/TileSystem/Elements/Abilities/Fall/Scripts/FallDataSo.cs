using _Game.TileSystem.Elements.Abilities.Shake.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.Elements.Abilities.Fall.Scripts
{
    [CreateAssetMenu(fileName = "FallDataSo", menuName = "Game/Element/Ability/Fall/Data")]
    public class FallDataSo : ScriptableObjectInstaller<ShakeDataSo>
    {
        public AnimationCurve animationCurve;
        public float duration;
        public float lastFallOffset;
        public float lastFallDuration;

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
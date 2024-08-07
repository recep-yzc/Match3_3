using _Game.TileSystem.AbilityModel.Shake.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.AbilityModel.Fall.Scripts
{
    [CreateAssetMenu(fileName = "FallDataSo", menuName = "Game/Tile/Ability/Fall/Data")]
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
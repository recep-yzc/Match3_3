using UnityEngine;
using Zenject;

namespace _Game.TileSystem.AbilityModel.Shake.Scripts
{
    [CreateAssetMenu(fileName = "ShakeDataSo", menuName = "Game/Tile/Ability/Shake/Data")]
    public class ShakeDataSo : ScriptableObjectInstaller<ShakeDataSo>
    {
        public AnimationCurve animationCurve;
        public float duration;
        public float force;

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
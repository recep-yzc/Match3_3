using UnityEngine;
using Zenject;

namespace _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts
{
    [CreateAssetMenu(fileName = "ScaleUpDownDataSo", menuName = "Game/Tile/Ability/ScaleUpDown/Data")]
    public class ScaleUpDownDataSo : ScriptableObjectInstaller<ScaleUpDownDataSo>
    {
        public AnimationCurve animationCurve;
        public float duration;
        public Vector3 force;

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
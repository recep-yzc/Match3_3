using UnityEngine;
using Zenject;

namespace _Game.TileSystem.Elements.Abilities.ScaleUpDown.Scripts
{
    [CreateAssetMenu(fileName = "ScaleUpDownDataSo", menuName = "Game/Element/Ability/ScaleUpDown/Data")]
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
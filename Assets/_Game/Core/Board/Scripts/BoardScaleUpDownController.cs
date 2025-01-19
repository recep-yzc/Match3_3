using _Game.Core.Abilities.ScaleUpDown.Scripts;
using _Game.Core.Grid.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardScaleUpDownController : MonoBehaviour
    {
        #region Parameters

        [Inject] private ScaleUpDownDataSo _scaleUpDownDataSo;

        #endregion

        public void TryScaleUpDown(GridData gridData)
        {
            gridData.GetGridComponents<IScaleUpDown>()?.ScaleUpDownAsync(_scaleUpDownDataSo).Forget();
        }
    }
}
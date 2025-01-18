using UnityEngine;

namespace _Game.TileSystem.Elements.Empty.Scripts
{
    public class EmptyController : MonoBehaviour
    {
        #region Parameters

        private static EmptyElementDataSo _emptyElementDataSo;

        #endregion

        [Header("References")] [SerializeField]
        private EmptyElementDataSo emptyElementDataSo;

        #region UnityActions

        private void Start()
        {
            _emptyElementDataSo = emptyElementDataSo;
        }

        #endregion

        public EmptyElementDataSo GetEmptyElementDataSo()
        {
            return _emptyElementDataSo;
        }
    }
}
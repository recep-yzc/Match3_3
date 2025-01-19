using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.None.Scripts
{
    public class NoneController : ElementControllerBase
    {
        [Header("References")] [SerializeField]
        private NoneElementDataSo noneElementDataSo;

        public NoneElementDataSo GetNoneElementDataSo()
        {
            return noneElementDataSo;
        }
    }
}
using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Empty.Scripts
{
    public class EmptyController : ElementControllerBase
    {
        [Header("References")] [SerializeField]
        private EmptyElementDataSo emptyElementDataSo;

        public EmptyElementDataSo GetEmptyElementDataSo()
        {
            return emptyElementDataSo;
        }
    }
}
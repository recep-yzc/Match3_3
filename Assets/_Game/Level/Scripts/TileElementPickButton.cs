using _Game.Core.Elements.Element.Scripts;
using _Game.Core.Grid.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Level.Scripts
{
    public class TileElementPickButton : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Image imgIcon;

        [SerializeField] private Button btnIcon;

        #region Private

        private ElementDataBaseSo _elementDataBaseSo;

        #endregion

        #region First

        private void Awake()
        {
            btnIcon.onClick.AddListener(OnClicked);
        }

        #endregion

        public void Init(ElementDataBaseSo elementDataBaseSo)
        {
            _elementDataBaseSo = elementDataBaseSo;
            imgIcon.sprite = elementDataBaseSo.GetElementDataBase().icon;
        }

        private void OnClicked()
        {
            GridGlobalValues.SelectedElementDataBaseSo = _elementDataBaseSo;
        }
    }
}
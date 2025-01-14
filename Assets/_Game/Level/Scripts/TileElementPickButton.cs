using _Game.TileSystem.Tile.Scripts;
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

        private ElementDataSo _elementDataSo;

        #endregion

        #region First

        private void Awake()
        {
            btnIcon.onClick.AddListener(OnClicked);
        }

        #endregion

        public void Init(ElementDataSo elementDataSo)
        {
            _elementDataSo = elementDataSo;
            imgIcon.sprite = elementDataSo.GetElementData().icon;
        }

        private void OnClicked()
        {
            TileConstants.SelectedElementDataSo = _elementDataSo;
        }
    }
}
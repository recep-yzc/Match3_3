using System.Collections.Generic;
using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Level.Scripts
{
    public class ElementEditController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TileElementPickButton tileElementPickPrefab;

        [SerializeField] private Transform parent;
        [SerializeField] private List<ElementDataBaseSo> elementDataList;

        private void Start()
        {
            foreach (var tilePropertyDataSo in elementDataList)
            {
                var levelTileProperty = Instantiate(tileElementPickPrefab, parent);
                levelTileProperty.Init(tilePropertyDataSo);
            }
        }
    }
}
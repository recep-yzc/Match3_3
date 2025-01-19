using System;
using _Game.Core.Elements.Element.Scripts;
using _Game.Core.Elements.Empty.Scripts;
using _Game.Core.Elements.Gem.Scripts;
using _Game.Core.Elements.None.Scripts;
using _Game.Core.Elements.Wood.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Level.Scripts
{
    [Serializable]
    public class LevelGridData
    {
        [HideInInspector] public Vector2 coordinate;
        [HideInInspector] public ElementId elementId;

        [SerializeReference] [TabGroup("TileData")] [HideIf(nameof(ElementId), ElementId.None)]
        public ElementDataBase elementDataBase;

        public LevelGridData()
        {
            ElementId = ElementId.None;
        }

        [ShowInInspector]
        [GUIColor(0.5f, 0f, 0f)]
        [PropertyOrder(-1)]
        public ElementId ElementId
        {
            get => elementId;
            set
            {
                elementId = value;
                elementDataBase = CreateElementData(elementId);

                if (elementDataBase != null) elementDataBase.elementId = elementId;
            }
        }

        private ElementDataBase CreateElementData(ElementId id)
        {
            return id switch
            {
                ElementId.None => new NoneElementData(),
                ElementId.Empty => new EmptyElementData(),
                ElementId.Gem => new GemElementData(),
                ElementId.Wood => new WoodElementData(),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, "Invalid TileId")
            };
        }
    }
}
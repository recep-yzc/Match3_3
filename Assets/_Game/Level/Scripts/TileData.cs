using System;
using _Game.TileSystem.Elements.Empty.Scripts;
using _Game.TileSystem.Elements.Gem.Scripts;
using _Game.TileSystem.Elements.None.Scripts;
using _Game.TileSystem.Elements.Wood.Scripts;
using _Game.TileSystem.Tile.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Level.Scripts
{
    [Serializable]
    public class TileData
    {
        [HideInInspector] public Vector2 coordinate;
        [HideInInspector] public TileId tileId;

        [SerializeReference] [TabGroup("TileData")] [HideIf(nameof(TileId), TileId.None)]
        public ElementData elementData;

        public TileData()
        {
            TileId = TileId.None;
        }

        [ShowInInspector]
        [GUIColor(0.5f, 0f, 0f)]
        [PropertyOrder(-1)]
        public TileId TileId
        {
            get => tileId;
            set
            {
                tileId = value;
                elementData = CreateElementData(tileId);

                if (elementData != null) elementData.tileId = tileId;
            }
        }

        private ElementData CreateElementData(TileId id)
        {
            return id switch
            {
                TileId.None => new NoneElementData(),
                TileId.Empty => new EmptyElementData(),
                TileId.Gem => new GemElementData(),
                TileId.Wood => new WoodElementData(),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, "Invalid TileId")
            };
        }
    }
}
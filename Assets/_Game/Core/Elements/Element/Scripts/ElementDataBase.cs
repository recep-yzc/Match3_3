using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Core.Elements.Element.Scripts
{
    [Serializable]
    public abstract class ElementDataBase
    {
        [Header("Base Parameters")] public ElementId elementId;

        [PreviewField] public Sprite icon;
    }
}
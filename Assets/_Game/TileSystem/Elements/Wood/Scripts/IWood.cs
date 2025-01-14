using UnityEngine;

namespace _Game.TileSystem.Elements.Wood.Scripts
{
    public interface IWood
    {
        public int Shield { get; set; }
        public void SetShield(int shield);
        public void SetSprite(Sprite icon);
    }
}
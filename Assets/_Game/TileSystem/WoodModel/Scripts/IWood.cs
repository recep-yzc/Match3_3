using _Game.TileSystem.TileModel.Scripts;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public interface IWood : ITile
    {
        public void SetShield(int shieldAmount);
    }
}
using _Game.Code.Scripts.TileSystem.Tile.Handlers;
using Zenject;

namespace _Game.Code.Scripts.ZenjectSystem
{
    public class AssetsInstaller : MonoInstaller<AssetsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TileHandler>().FromResource("Tile").AsCached();
        }
    }
}
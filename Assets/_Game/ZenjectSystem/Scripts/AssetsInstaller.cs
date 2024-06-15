using _Game.TileSystem.TileModel.Scripts;
using Zenject;

namespace _Game.ZenjectSystem.Scripts
{
    public class AssetsInstaller : MonoInstaller<AssetsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Tile>().FromResource("Tile").AsCached();
        }
    }
}
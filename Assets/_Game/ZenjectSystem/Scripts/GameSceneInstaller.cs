using _Game.BoardSystem.BoardModel.Scripts;
using _Game.TileSystem.EmptyModel.Scripts;
using _Game.TileSystem.GemModel.Scripts;
using _Game.TileSystem.WoodModel.Scripts;
using Zenject;

namespace _Game.ZenjectSystem.Scripts
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInstance(FindObjectOfType<GemFactory>());
            Container.BindInstance(FindObjectOfType<WoodFactory>());
            Container.BindInstance(FindObjectOfType<EmptyFactory>());

            Container.BindInstance(FindObjectOfType<BoardController>());
            Container.BindInstance(FindObjectOfType<BoardInputController>());

            Container.BindInstance(FindObjectOfType<BoardBlastController>());
            Container.BindInstance(FindObjectOfType<BoardShakeController>());
            Container.BindInstance(FindObjectOfType<BoardScaleUpDownController>());
            Container.BindInstance(FindObjectOfType<BoardFallController>());
        }
    }
}
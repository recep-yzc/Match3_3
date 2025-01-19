using _Game.Core.Board.Scripts;
using _Game.Core.Elements.Empty.Scripts;
using _Game.Core.Elements.Gem.Scripts;
using _Game.Core.Elements.None.Scripts;
using _Game.Core.Elements.Wood.Scripts;
using Zenject;

namespace _Game.ZenjectSystem.Scripts
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInstance(FindObjectOfType<NoneController>());
            Container.BindInstance(FindObjectOfType<EmptyController>());
            Container.BindInstance(FindObjectOfType<GemController>());
            Container.BindInstance(FindObjectOfType<WoodController>());
            
            Container.BindInstance(FindObjectOfType<NoneFactory>());
            Container.BindInstance(FindObjectOfType<EmptyFactory>());
            Container.BindInstance(FindObjectOfType<GemFactory>());
            Container.BindInstance(FindObjectOfType<WoodFactory>());
            
            
            Container.BindInstance(FindObjectOfType<BoardController>());
            
            Container.BindInstance(FindObjectOfType<BoardSpawnController>());
            Container.BindInstance(FindObjectOfType<BoardViewController>());
            Container.BindInstance(FindObjectOfType<BoardInputController>());

            Container.BindInstance(FindObjectOfType<BoardBlastController>());
            Container.BindInstance(FindObjectOfType<BoardFallController>());

            Container.BindInstance(FindObjectOfType<BoardShakeController>());
            Container.BindInstance(FindObjectOfType<BoardScaleUpDownController>());
        }
    }
}
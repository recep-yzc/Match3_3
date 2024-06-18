using _Game.BoardSystem.BoardModel.Scripts;
using Zenject;

namespace _Game.ZenjectSystem.Scripts
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInstance(FindObjectOfType<BoardController>());
            Container.BindInstance(FindObjectOfType<BoardInputController>());

            Container.BindInstance(FindObjectOfType<BoardBlastController>());
            Container.BindInstance(FindObjectOfType<BoardShakeController>());
            Container.BindInstance(FindObjectOfType<BoardScaleUpDownController>());
        }
    }
}
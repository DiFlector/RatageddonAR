using UnityEngine;
using Zenject;

public class DependenciesInstaller : MonoInstaller
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private Player _player;
    [SerializeField] private Canon _canon;


    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
        Container.Bind<ViewManager>().FromInstance(_viewManager).AsSingle().NonLazy();
        Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        Container.Bind<Canon>().FromInstance(_canon).AsSingle().NonLazy();
    }
}

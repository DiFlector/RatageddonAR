using Zenject;
using UnityEngine;

public class DependenciesInstaller : MonoInstaller
{
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private GameManager _gameManager;


    public override void InstallBindings()
    {
        Container.Bind<ViewManager>().FromInstance(_viewManager).AsSingle().NonLazy();
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
    }
}

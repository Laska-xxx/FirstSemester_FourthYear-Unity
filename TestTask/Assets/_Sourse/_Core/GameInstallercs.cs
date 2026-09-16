using Core;
using Environment.Asteroids;
using Player;
using UnityEngine;
using Zenject;

public class GameInstallercs : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private PlayerController player;

    [Header("Asteroids")]
    [SerializeField] private AsteroidFactory asteroidFactory;
    [SerializeField] private GameObject asteroidPrefab;

    public override void InstallBindings()
    {
        CoreInstaller();
        PlayerInstaller();
        FactoryInstaller();
    }

    private void CoreInstaller()
    {
        Container.BindInterfacesAndSelfTo<InputListener>().AsSingle();
        Container.BindInterfacesAndSelfTo<Score.ScoreManager>().AsSingle();
    }

    private void PlayerInstaller()
    {
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
        Container.Bind<AsteroidFactory>().FromInstance(asteroidFactory).AsSingle();
    }

    private void FactoryInstaller()
    {
        Container.BindMemoryPool<Asteroid, Asteroid.Pool>().WithInitialSize(4).FromComponentInNewPrefab(asteroidPrefab);
    }
}
using Core;
using Environment.Asteroids;
using Environment.Boosters;
using GameState;
using Player;
using Score;
using System;
using UnityEngine;
using Zenject;

public class GameInstallercs : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private PlayerController player;

    [Header("Asteroids")]
    [SerializeField] private AsteroidFactory asteroidFactory;
    [SerializeField] private Asteroid asteroidPrefab;

    [Header("Boosters")]
    [SerializeField] private BoosterFactory boosterFactory;
    [SerializeField] private Booster boosterPrefab;

    public override void InstallBindings()
    {
        CoreInstaller();
        PlayerInstaller();
        FactoryInstaller();
    }

    private void CoreInstaller()
    {
        Container.BindInterfacesAndSelfTo<InputListener>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        Container.Bind<StateMachine>().AsSingle();
        Container.Bind<GameContext>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameFlowController>().AsSingle().NonLazy();
    }

    private void PlayerInstaller()
    {
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();

        Container.Bind<AsteroidFactory>().FromInstance(asteroidFactory).AsSingle();
        Container.Bind<BoosterFactory>().FromInstance(boosterFactory).AsSingle();
    }

    private void FactoryInstaller()
    {
        Container.BindMemoryPool<Asteroid, Asteroid.AsteroidPool>().WithInitialSize(4).FromComponentInNewPrefab(asteroidPrefab);
        Container.BindMemoryPool<Booster, Booster.BoosterPool>().WithInitialSize(3).FromComponentInNewPrefab(boosterPrefab);
    }
}
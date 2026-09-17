using Core;
using Environment.Asteroids;
using Environment.Boosters;
using GameState;
using Player;
using Score;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [Header("Player")]
    [SerializeField] private PlayerController player;

    [Header("Asteroids")]
    [SerializeField] private AsteroidFactory asteroidFactory;
    [SerializeField] private Asteroid asteroidPrefab;

    [Header("Boosters")]
    [SerializeField] private BoosterFactory boosterFactory;
    [SerializeField] private Booster boosterPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        CoreRegister(builder);
        PlayerRegister(builder);
        PoolsRegister(builder);
    }

    private void CoreRegister(IContainerBuilder builder)
    {
        builder.Register<InputListener>(Lifetime.Singleton).As<IInputListener>();
        builder.Register<ScoreManager>(Lifetime.Singleton);
        builder.Register<StateMachine>(Lifetime.Singleton);
        builder.Register<GameContext>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameFlowController>();
    }

    private void PlayerRegister(IContainerBuilder builder)
    {
        builder.RegisterComponent(player);

        builder.RegisterComponent(asteroidFactory);
        builder.RegisterComponent(boosterFactory);
    }

    private void PoolsRegister(IContainerBuilder builder)
    {
        builder.Register<AsteroidPool>(Lifetime.Singleton).WithParameter(asteroidPrefab);
        builder.Register<BoosterPool>(Lifetime.Singleton).WithParameter(boosterPrefab);
    }
}
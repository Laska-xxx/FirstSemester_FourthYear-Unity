using Core;
using Player;
using UnityEngine;
using Zenject;

public class GameInstallercs : MonoInstaller
{
    [SerializeField] private PlayerController player;

    public override void InstallBindings()
    {
        CoreInstaller();
        PlayerInstaller();
    }

    private void CoreInstaller()
    {
        Container.BindInterfacesAndSelfTo<InputListener>().AsSingle();
        Container.BindInterfacesAndSelfTo<Score.ScoreManager>().AsSingle();
    }

    private void PlayerInstaller()
    {
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
    }
}
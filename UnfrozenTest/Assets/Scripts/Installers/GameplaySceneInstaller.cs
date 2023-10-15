using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    //Использую преимущественно Zenject как DI-Container, по-хорошему объекты изначально не должны быть на сцене, и мы их должны создавать, можем обращаться по Resources.Load и искать по пути
    [Header("StartInfo")]
    [SerializeField] private HeroType _startHero;
    [Header("Objects on Scene")]
    [SerializeField] private MissionBattlePanel _battlePanel;
    [SerializeField] private HeroPanel _heroPanel;
    [SerializeField] private MissionDescriptionPanel _missionDescriptionPanel;
    [SerializeField] private Map _map;

    public override void InstallBindings()
    {
        Container.Bind<MissionBattlePanel>().FromInstance(_battlePanel);
        Container.Bind<HeroType>().FromInstance(_startHero).AsSingle();
        Container.Bind<HeroPanel>().FromInstance(_heroPanel).AsSingle();
        Container.Bind<MissionDescriptionPanel>().FromInstance(_missionDescriptionPanel);
        Container.Bind<Map>().FromInstance(_map);
    }
}

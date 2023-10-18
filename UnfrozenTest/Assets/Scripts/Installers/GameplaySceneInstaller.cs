using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    //Использую преимущественно Zenject как DI-Container, по-хорошему объекты изначально не должны быть на сцене, и мы их должны создавать, можем обращаться по Resources.Load и искать по пути
    //Вообще можно было проще написать, условный класс BootStrap и оттуда прокинуть зависимости, особой разницы нет
    [Header("StartInfo")]
    [SerializeField] private HeroType _startHero;
    [Header("Objects on Scene")]
    [SerializeField] private MissionBattlePanel _battlePanel;
    [SerializeField] private HeroPanel _heroPanel;
    [SerializeField] private MissionDescriptionPanel _missionDescriptionPanel;
    [SerializeField] private Map _map;

    public override void InstallBindings()
    {
        Container.Bind<IBattlePanel>().To<MissionBattlePanel>().FromInstance(_battlePanel);
        Container.Bind<HeroType>().FromInstance(_startHero).AsSingle();
        Container.Bind<IHeroPanel>().To<HeroPanel>().FromInstance(_heroPanel).AsSingle();
        Container.Bind<IMissionDescription>().To<MissionDescriptionPanel>().FromInstance(_missionDescriptionPanel);
        Container.Bind<IMap>().To<Map>().FromInstance(_map);
    }
}

using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    //��������� ��������������� Zenject ��� DI-Container, ��-�������� ������� ���������� �� ������ ���� �� �����, � �� �� ������ ���������, ����� ���������� �� Resources.Load � ������ �� ����
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

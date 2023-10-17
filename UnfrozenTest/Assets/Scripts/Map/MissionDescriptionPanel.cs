using TMPro;
using UnityEngine;
using Zenject;
//Скриптик для отображения информации о миссиии
public class MissionDescriptionPanel : MonoBehaviour, IMissionDescription
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private PlayButton _playButton;
    [SerializeField] private InfoPopup _infoPopup;
    
    private MissionData _previousMissionData;
    private IBattlePanel _battlePanel;
    private IHeroPanel _heroPanel;

    [Inject]
    public void Constructor(IBattlePanel missionBattlePanel, IHeroPanel heroPanel)
    {
        _battlePanel = missionBattlePanel;
        _heroPanel = heroPanel;
    }
    //Отображаем информации
    public void Show(IMissionView missionData)
    {
        if(missionData.MissionState == MissionState.Active)
        {
            _name.text = missionData.MissionData.MissionName;
            _description.text = missionData.MissionData.DescriptionName;
            _previousMissionData = missionData.MissionData;
            _playButton.OnInteractable();
            _playButton.AddListener(StartPlay, _previousMissionData);
            _playButton.AddListener(missionData.GetNextState);
        }
        else
        {
            _playButton.OffInteractable();
        }
    }

    //Метод для добавления в playButton, если герой не выбран, то показываем сообщение об этом.
    public void StartPlay(MissionData missionData)
    {
        if (_heroPanel.SelectedHeroView == null)
        {
            _infoPopup.Show();
        }
        else
        {
            _battlePanel.StartBattle(missionData);
            _playButton.RemoveAllListeners();
        }
    }
}

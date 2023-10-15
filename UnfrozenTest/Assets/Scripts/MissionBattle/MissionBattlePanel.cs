using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionBattlePanel : MonoBehaviour
{
    private const string ForWhomText = "За кого играем: ";
    private const string AgainstText = "Против кого играем: ";

    [SerializeField] private TMP_Text _missionNameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _missionWhom;
    [SerializeField] private TMP_Text _missionAgainst;
    [SerializeField] private Button _finishButton;

    private HeroPanel _heroPanel;
    private Map _map;

    [Inject]
    public void Constructor(HeroPanel heroPanel, Map map)
    {
        _map = map;
        _heroPanel = heroPanel;
    }

    public void StartBattle(MissionData missionData)
    {
        gameObject.SetActive(true);
        UpdateTexts(missionData);

        _finishButton.onClick.RemoveAllListeners();
        _finishButton.onClick.AddListener(() =>
        {
            _map.OnMissionComplete(missionData);
            _heroPanel.OnMissionComplete(missionData.UnlockableHeroes, missionData.AddedCharacterPoints);
            gameObject.SetActive(false);
        });
    }

    private void UpdateTexts(MissionData missionData)
    {
        _missionNameText.text = missionData.MissionName;
        _descriptionText.text = missionData.MissionText;
        _missionWhom.text = $"{ForWhomText}{missionData.ForWhomPlay}";
        _missionAgainst.text = $"{AgainstText}{missionData.AgaintsPlay}";
    }
}

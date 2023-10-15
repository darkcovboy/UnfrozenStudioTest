using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Missions/Mission")]
public class MissionData : ScriptableObject, IData
{
    /*
     * Собственно, насчет двойной миссии была самая большая проблема, становилось несколько проблем, мы либо создаем некий абстрактный MissionData, а от него наследуем SingleMissionData
     * и DoubleMissionData, где во втором создаем копии двух одиночных миссий, но мне показалось, что подобное будет смотреться чуть перегружено, поэтому решил добавить bool, который за отвечает
     * и вставлять ссылку на миссию, с которой коннектимся, пользуясь Odin'ом, можно было бы её красиво скрыть)
     * Также не совсем понял зачем нам TemporaryBlockedMissions...
     * Большинство можно было бы сделать свойствами через [SerializeField: field], но по опыту, такие штуки Unity очень любит странно сохранять, поэтому обычно пишу так сначала поле, а пото свойство get к нему
    */
    [Header("Main information")]
    [SerializeField] private float _index;
    [SerializeField] private bool _isSingleMission;
    [Header("Can be null if isSingleMission true")]
    [SerializeField] private MissionData _connectedMission;
    [Header("Text For Interface and Position")]
    [SerializeField] private Vector2 _position;
    [SerializeField] private string _missionName;
    [SerializeField] private string _descriptionName;
    [SerializeField] private string _missionText;
    [SerializeField] private string _forWhomPlaying;
    [SerializeField] private string _againsWhoPlaying;
    [Header("Gameplay")]
    [SerializeField] private List<MissionData> _temporaryBlockedMissions;
    [SerializeField] private List<MissionData> _neededMissionsToOpen;
    [SerializeField] private List<HeroType> _unlockableHeroes;
    [SerializeField] private List<AddedCharacterPoints> _addedCharacterPoints;

    private List<MissionData> _neededMissionsToOpenCopy;

    public float Index => _index;
    public bool IsSingleMission => _isSingleMission;
    public MissionData ConnectedMission => _connectedMission;
    public Vector2 Position => _position;
    public string MissionName => _missionName;
    public string DescriptionName => _descriptionName;
    public string MissionText => _missionText;
    public string ForWhomPlay => _forWhomPlaying;
    public string AgaintsPlay => _againsWhoPlaying;

    public IEnumerable<MissionData> TemporaryBlockedMissions => _temporaryBlockedMissions;
    public IEnumerable<MissionData> NeededMissionsToOpen => _neededMissionsToOpenCopy;

    public IEnumerable<HeroType> UnlockableHeroes => _unlockableHeroes;

    public IEnumerable<AddedCharacterPoints> AddedCharacterPoints => _addedCharacterPoints;

    //Для работы создаем копии объектов, чтобы их можно было изменять
    public void CreateCopy()
    {
        _neededMissionsToOpenCopy = new List<MissionData>(_neededMissionsToOpen);
    }

    //Убираем из нужных для открытия миссий ненужные миссии
    public void RemoveUnreachableMission(MissionData missionData)
    {
        _neededMissionsToOpenCopy.Remove(missionData);
    }

    //Добавил проверку, что если у нас миссия двойная, то _connectedMission не должно быть null 
    private void OnValidate()
    {
        if (_isSingleMission == false && _connectedMission == null)
            throw new NullReferenceException();
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Missions/Mission")]
public class MissionData : ScriptableObject, IData
{
    /*
     * ����������, ������ ������� ������ ���� ����� ������� ��������, ����������� ��������� �������, �� ���� ������� ����� ����������� MissionData, � �� ���� ��������� SingleMissionData
     * � DoubleMissionData, ��� �� ������ ������� ����� ���� ��������� ������, �� ��� ����������, ��� �������� ����� ���������� ���� �����������, ������� ����� �������� bool, ������� �� ��������
     * � ��������� ������ �� ������, � ������� �����������, ��������� Odin'��, ����� ���� �� � ������� ������)
     * ����� �� ������ ����� ����� ��� TemporaryBlockedMissions...
     * ����������� ����� ���� �� ������� ���������� ����� [SerializeField: field], �� �� �����, ����� ����� Unity ����� ����� ������� ���������, ������� ������ ���� ��� ������� ����, � ���� �������� get � ����
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

    //��� ������ ������� ����� ��������, ����� �� ����� ���� ��������
    public void CreateCopy()
    {
        _neededMissionsToOpenCopy = new List<MissionData>(_neededMissionsToOpen);
    }

    //������� �� ������ ��� �������� ������ �������� ������
    public void RemoveUnreachableMission(MissionData missionData)
    {
        _neededMissionsToOpenCopy.Remove(missionData);
    }

    //������� ��������, ��� ���� � ��� ������ �������, �� _connectedMission �� ������ ���� null 
    private void OnValidate()
    {
        if (_isSingleMission == false && _connectedMission == null)
            throw new NullReferenceException();
    }
}

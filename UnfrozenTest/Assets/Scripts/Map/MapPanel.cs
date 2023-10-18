using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private MissionFactory _missionFactory;

    private List<Mission> _missions = new List<Mission>();
    private IMissionDescription _missionDescriptionPanel;

    [Inject]
    public void Constructor(IMissionDescription missionDescriptionPanel)
    {
        _missionDescriptionPanel = missionDescriptionPanel;
    }

    //�����, ������� ������� ������� ������, ������������� �� �������, ��������� ��������, ��������� � ���� ��� ��������.
    public void Show(IEnumerable<MissionData> missionDatas)
    {
        foreach (var missionData in missionDatas)
        {
            MissionView missionView = _missionFactory.Get(container);
            Mission mission = new Mission(missionData, missionView);

            _missions.Add(mission);
            mission.MissionView.OnClick += _missionDescriptionPanel.Show;
        }
    }

    //��������� ������ ��� ������
    public void UpdateStates(MissionData CompleteMission)
    {
        //���� � ��� ������� ������, �� ��� ������ ������ ��������� ������ ������, �, ��������������, ������� �����������
        if (CompleteMission.IsSingleMission == false)
            RemoveUnreachebleMissions(CompleteMission.ConnectedMission);

        Mission mission = _missions.First<Mission>(mission => mission.MissionData == CompleteMission);

        mission.GetNextState();

        CheckMissionUnlock(CompleteMission);
    }

    //����� ����� ��� ��������� ������� �����������, ���������� ����� ���������� ���������� �� ������ �� ������� ������ � ������ �� � ������ �������
    //������ �������� ����� ������ ���������� �� O, �� ����������, ��� �� ���-���� � ������� ����� ����� �� 1000 ������))
    private void RemoveUnreachebleMissions(MissionData missionToRemove)
    {
        foreach (var mission in _missions)
        {
            if (mission.NeededMissionsToOpen.Contains(missionToRemove))
            {
                mission.RemoveUnreachableMission(missionToRemove);

                if (mission.NeededMissionsToOpen.Count() == 0)
                {
                    RemoveUnreachebleMissions(mission.MissionData);
                }
            }
        }
    }

    //���� ������ ��������, �� ��������� ����� ������ ��������� � ������ �� ���������.
    private void CheckMissionUnlock(MissionData completeMission)
    {
        foreach (Mission mission in _missions)
        {
            MissionData missionData = mission.MissionData;

            if (completeMission.IsSingleMission == false)
            {
                if(mission.MissionData == completeMission.ConnectedMission)
                {
                    mission.Block();
                }
            }

            if (missionData.NeededMissionsToOpen.Contains(completeMission))
            {
                bool allNeededMissionsComplete = missionData.NeededMissionsToOpen.All(neededMission => IsMissionCompleted(neededMission));

                if (allNeededMissionsComplete)
                {
                    mission.GetNextState();
                }
            }
        }
    }
    //������� �������� �� � ��� ������, ����� ����� ��������� 
    private bool IsMissionCompleted(MissionData missionData)
    {
        Mission mission = _missions.FirstOrDefault(view => view.MissionData == missionData);

        if (mission != null)
        {
            return mission.MissionState == MissionState.Passed;
        }

        return false;
    }
}

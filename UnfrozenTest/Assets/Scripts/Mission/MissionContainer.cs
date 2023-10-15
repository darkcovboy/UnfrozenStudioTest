using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Container")]
public class MissionContainer : ScriptableObject
{
    [SerializeField] private List<MissionData> _missions;

    public List<MissionData> Missions => _missions;

    //����� ����� ��� ��������� ������� �����������, ���������� ����� ���������� ���������� �� ������ �� ������� ������ � ������ �� � ������ �������
    //������ �������� ����� ������ ���������� �� O, �� ����������, ��� �� ���-���� � ������� ����� ����� �� 1000 ������)) �� 
    public void RemoveUnreachebleMissions(MissionData missionToRemove)
    {
        foreach (var mission in _missions)
        {
            if(mission.NeededMissionsToOpen.Contains(missionToRemove))
            {
                mission.RemoveUnreachableMission(missionToRemove);

                if(mission.NeededMissionsToOpen.Count() == 0)
                {
                    RemoveUnreachebleMissions(mission);
                }
            }
        }
    }
}

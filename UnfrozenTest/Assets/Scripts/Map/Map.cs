using UnityEngine;

//� ���� ������� �������� � MapPanel ����� ������, � ��� ������ � ������ �������� onMissionComplete
public class Map : MonoBehaviour, IMap
{
    [SerializeField] private MissionContainer missionContainer;
    [SerializeField] private MapPanel mapPanel;

    private void Start()
    {
        mapPanel.Show(missionContainer.Missions);
    }

    public void OnMissionComplete(MissionData missionData)
    {
        //���� � ��� ������� ������, �� ��� ������ ������ ��������� ������ ������, �, ��������������, ������� �����������
        if (missionData.IsSingleMission == false)
            missionContainer.RemoveUnreachebleMissions(missionData.ConnectedMission);

        //��������� ��������� ��� ���������� ������
        mapPanel.UpdateStates(missionData);
        //������� ����� ������ � ��� ���������
        mapPanel.CheckMissionUnlock(missionData);
    }
}

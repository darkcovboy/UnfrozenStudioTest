using UnityEngine;

//В этом скрипте вызываем у MapPanel показ миссий, и при победе в миссии вызываем onMissionComplete
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
        //Обновляем состояние для пройденной миссии
        mapPanel.UpdateStates(missionData);
    }
}

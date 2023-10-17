using UnityEngine;

//¬ этом скрипте вызываем у MapPanel показ миссий, и при победе в миссии вызываем onMissionComplete
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
        //≈сли у нас двойна€ мисси€, то дл€ другой миссии блокируем другие уровни, и, соответственно, удал€ем зависимости
        if (missionData.IsSingleMission == false)
            missionContainer.RemoveUnreachebleMissions(missionData.ConnectedMission);

        //ќбновл€ем состо€ние дл€ пройденной миссии
        mapPanel.UpdateStates(missionData);
        //—мотрим какие миссии у нас открылись
        mapPanel.CheckMissionUnlock(missionData);
    }
}

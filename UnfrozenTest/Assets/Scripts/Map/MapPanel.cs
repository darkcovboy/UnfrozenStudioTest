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

    //Метод, который создает объекты миссий, устанавливаем им позицию, добавляем подписки, добавляем в лист для хранения.
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

    //Обновляем стейты для миссии
    public void UpdateStates(MissionData CompleteMission)
    {
        //Если у нас двойная миссия, то для другой миссии блокируем другие уровни, и, соответственно, удаляем зависимости
        if (CompleteMission.IsSingleMission == false)
            RemoveUnreachebleMissions(CompleteMission.ConnectedMission);

        Mission mission = _missions.First<Mission>(mission => mission.MissionData == CompleteMission);

        mission.GetNextState();

        CheckMissionUnlock(CompleteMission);
    }

    //Долго думал где конкретно чистить зависимости, собственно здесь рекурсивно проходимся по связам от двойной миссии и чистим их в других миссиях
    //Обычно рекурсия может сильно отжираться по O, но сомневаюсь, что мы все-таки в будущем будем иметь от 1000 миссий))
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

    //Если миссия пройдена, то проверяем какие миссии открылись и меняем им состояния.
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
    //Смотрим пройдена ли у нас миссия, нужно чтобы открывать 
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

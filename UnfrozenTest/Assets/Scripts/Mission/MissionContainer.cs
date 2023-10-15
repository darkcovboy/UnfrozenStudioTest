using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Container")]
public class MissionContainer : ScriptableObject
{
    [SerializeField] private List<MissionData> _missions;

    public List<MissionData> Missions => _missions;

    //Долго думал где конкретно чистить зависимости, собственно здесь рекурсивно проходимся по связам от двойной миссии и чистим их в других миссиях
    //Обычно рекурсия может сильно отжираться по O, но сомневаюсь, что мы все-таки в будущем будем иметь от 1000 миссий)) Но 
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

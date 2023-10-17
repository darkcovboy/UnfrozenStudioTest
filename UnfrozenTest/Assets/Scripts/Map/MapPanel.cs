using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private MissionFactory _missionFactory;

    private List<MissionView> missionViews = new List<MissionView>();
    private IMissionDescription _missionDescriptionPanel;

    [Inject]
    public void Constructor(IMissionDescription missionDescriptionPanel)
    {
        _missionDescriptionPanel = missionDescriptionPanel;
    }

    //ћетод, который создает объекты миссий, устанавливаем им позицию, добавл€ем подписки, добавл€ем в лист дл€ хранени€.
    public void Show(IEnumerable<MissionData> missionDatas)
    {
        foreach (var mission in missionDatas)
        {
            MissionView missionView = _missionFactory.Get(mission, container);
            missionView.GetComponent<RectTransform>().anchoredPosition = mission.Position;
            missionView.OnClick += _missionDescriptionPanel.Show;
            missionViews.Add(missionView);
        }
    }

    //ќбновл€ем стейты дл€ миссии
    public void UpdateStates(MissionData CompleteMission)
    {
        MissionView missionView = missionViews.First<MissionView>(mission => mission.MissionData == CompleteMission);

        missionView.GetNextState();
    }

    //≈сли мисси€ пройдена, то провер€ем какие миссии открылись и мен€ем им состо€ни€.
    public void CheckMissionUnlock(MissionData completeMission)
    {
        foreach (MissionView missionView in missionViews)
        {
            MissionData missionData = missionView.MissionData;

            if (completeMission.IsSingleMission == false)
            {
                if(missionView.MissionData == completeMission.ConnectedMission)
                {
                    missionView.Block();
                }
            }

            if (missionData.NeededMissionsToOpen.Contains(completeMission))
            {
                bool allNeededMissionsComplete = missionData.NeededMissionsToOpen.All(neededMission => IsMissionCompleted(neededMission));

                if (allNeededMissionsComplete)
                {
                    missionView.GetNextState();
                }
            }
        }
    }
    //—мотрим пройдена ли у нас мисси€, нужно чтобы открывать 
    private bool IsMissionCompleted(MissionData mission)
    {
        MissionView missionView = missionViews.FirstOrDefault(view => view.MissionData == mission);

        if (missionView != null)
        {
            return missionView.MissionState == MissionState.Passed;
        }

        return false;
    }
}

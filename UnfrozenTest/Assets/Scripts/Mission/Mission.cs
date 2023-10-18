using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    private const float FirstIndex = 1;

    public MissionData MissionData { get; private set; }
    public MissionState MissionState { get; private set; }

    public MissionView MissionView { get; private set; }

    public IEnumerable<MissionData> NeededMissionsToOpen => _neededMissionsToOpenCopy;

    private List<MissionData> _neededMissionsToOpenCopy;

    public Mission(MissionData missionData, MissionView missionView)
    {
        MissionData = missionData;
        MissionView = missionView;
        missionView.Initialize(this);
        MissionView.UpdateText(missionData.Index.ToString());
        missionView.GetComponent<RectTransform>().anchoredPosition = missionData.Position;
        MissionState = MissionState.Locked;
        CreateCopy();

        if (missionData.Index == FirstIndex)
            GetNextState();
        else
            MissionView.ChangeColor(MissionState);
    }

    public void GetNextState()
    {
        switch (MissionState)
        {
            case MissionState.Locked:
                MissionState = MissionState.Active;
                MissionView.ChangeColor(MissionState);
                break;
            case MissionState.Active:
                MissionState = MissionState.TemporarilyLocked;
                MissionView.ChangeColor(MissionState);
                break;
            case MissionState.TemporarilyLocked:
                MissionState = MissionState.Passed;
                MissionView.ChangeColor(MissionState);
                break;
        }
    }

    public void RemoveUnreachableMission(MissionData missionData)
    {
        _neededMissionsToOpenCopy.Remove(missionData);
    }

    public void Block()
    {
        MissionView.Block();
    }

    private void CreateCopy()
    {
        _neededMissionsToOpenCopy = new List<MissionData>(MissionData.NeededMissionsToOpen);
    }
}

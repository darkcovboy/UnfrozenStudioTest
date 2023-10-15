public interface IMissionView
{
    MissionData MissionData { get;}
    MissionState MissionState { get;}

    void GetNextState();
}

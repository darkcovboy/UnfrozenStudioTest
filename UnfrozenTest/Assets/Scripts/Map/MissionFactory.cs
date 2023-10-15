using UnityEngine;

[CreateAssetMenu(fileName = "MissionFactory", menuName = "Factory/MissionFactory")]
public class MissionFactory : ScriptableObject, IFactory<MissionView, MissionData>
{
    private const float IndexFirstLevel = 1;

    [SerializeField] private MissionView _missionViewPrefab;

    public MissionView Get(MissionData missionData, Transform parent)
    {
        MissionView instance = Instantiate(_missionViewPrefab, parent);
        //“ак как изменени€ в ScriptableObject у нас солит изменени€ми навсегда, то создаем копии некоторых объектов, чтобы мы могли их спокойно измен€ть
        missionData.CreateCopy();

        if(missionData.Index != IndexFirstLevel)
            instance.Initialize(missionData);
        else
            instance.Initialize(missionData, true);

        return instance;
    }
}

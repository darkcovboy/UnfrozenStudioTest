using UnityEngine;

[CreateAssetMenu(fileName = "MissionFactory", menuName = "Factory/MissionFactory")]
public class MissionFactory : ScriptableObject, IFactory<MissionView>
{
    [SerializeField] private MissionView _missionViewPrefab;

    public MissionView Get(Transform parent)
    {
        MissionView instance = Instantiate(_missionViewPrefab, parent);
        return instance;
    }
}

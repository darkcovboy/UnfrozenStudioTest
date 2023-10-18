using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Container")]
public class MissionContainer : ScriptableObject
{
    [SerializeField] private List<MissionData> _missions;

    public List<MissionData> Missions => _missions;
}

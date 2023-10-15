using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Container")]
public class HeroContainer : ScriptableObject
{
    //Реализуем контейнер для персонажей, чтобы мы могли их хранить.
    [SerializeField] private List<HeroData> _heroes;

    public IEnumerable<HeroData> Heroes => _heroes;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Hero/HeroData")]
public class HeroData : ScriptableObject, IData
{
    //HeroData наследуем от IData, чтобы мы могли далее с ней унифицированно рабоать + храним данные о герое, score можно было бы выкинуть, наверно, ведь логично всегда начинать с 0
    //но мало ли нам нужно начинать не с 0.
    [SerializeField] private string _name;
    [SerializeField] private HeroType _heroType;
    [SerializeField] private int _score = 0;


    public string Name => _name;
    public HeroType HeroType => _heroType;
    public int Score => _score;
}

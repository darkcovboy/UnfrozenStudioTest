using UnityEngine;

[CreateAssetMenu(menuName = "Hero/HeroData")]
public class HeroData : ScriptableObject, IData
{
    //HeroData ��������� �� IData, ����� �� ����� ����� � ��� �������������� ������� + ������ ������ � �����, score ����� ���� �� ��������, �������, ���� ������� ������ �������� � 0
    //�� ���� �� ��� ����� �������� �� � 0.
    [SerializeField] private string _name;
    [SerializeField] private HeroType _heroType;
    [SerializeField] private int _score = 0;


    public string Name => _name;
    public HeroType HeroType => _heroType;
    public int Score => _score;
}

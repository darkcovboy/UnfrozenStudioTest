using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Factory")]
public class HeroFactory : ScriptableObject, IFactory<HeroView, HeroData>
{
    //Фабрика для создания HeroView
    [SerializeField] private HeroView _heroView;
    public HeroView Get(HeroData data, Transform parent)
    {
        HeroView instance = Instantiate(_heroView, parent);
        instance.Initialize(data);
        return instance;
    }
}

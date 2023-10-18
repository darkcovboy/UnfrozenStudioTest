using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Factory")]
public class HeroFactory : ScriptableObject, IFactory<HeroView>
{
    //Фабрика для создания HeroView
    [SerializeField] private HeroView _heroView;
    public HeroView Get(Transform parent)
    {
        HeroView instance = Instantiate(_heroView, parent);
        return instance;
    }
}

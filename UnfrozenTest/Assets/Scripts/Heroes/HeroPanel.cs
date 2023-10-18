using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class HeroPanel : MonoBehaviour, IHeroPanel
{
    //ѕанелька с геро€ми, часть логики можно было бы вынести вне монобеха, но с учетом, что еЄ здесь мало, да и делать надо было быстро, поэтому решил оставить здесь
    [SerializeField] private HeroContainer _heroContainer;
    [SerializeField] private HeroFactory _heroFactory;
    [SerializeField] private Transform _container;

    private List<Hero> _heroes = new List<Hero>();
    public Hero SelectedHeroView { get; private set; }

    private HeroType _startHero;

    [Inject]
    public void Constructor(HeroType heroType)
    {
        _startHero = heroType;
    }

    private void Start()
    {
        Show();
    }

    public void OnMissionComplete(IEnumerable<HeroType> heroesUnlock, IEnumerable<AddedCharacterPoints> addedCharacterPoints)
    {
        if(heroesUnlock.Count() > 0)
        {
            foreach (HeroType heroType in heroesUnlock)
            {
                Hero hero = _heroes.First(view => view.HeroData.HeroType == heroType);
                hero.Lock(false);
            }
        }

        if(addedCharacterPoints.Count() > 0)
        {
            foreach (AddedCharacterPoints characterPoints in addedCharacterPoints)
            {
                if(characterPoints.HeroType == HeroType.CurrentHero)
                {
                    Hero heroView = _heroes.First(view => view.IsSelected == true);
                    heroView.UpdateScore(characterPoints.Points);
                }
                else
                {
                    Hero heroView = _heroes.First(view => view.HeroData.HeroType == characterPoints.HeroType);
                    heroView.UpdateScore(characterPoints.Points);
                }
            }
        }
    }

    private void Show()
    {
        foreach (var heroData in _heroContainer.Heroes)
        {
            HeroView heroView = _heroFactory.Get(_container);
            Hero hero = new Hero(heroData, heroView);
            _heroes.Add(hero);
            heroView.OnHeroViewClicked += SelectHero;
        }

        foreach (var hero in _heroes)
        {
            if (hero.HeroData.HeroType == _startHero)
                hero.Lock(false);
        }
    }

    private void SelectHero(Hero heroToSelect)
    {
        if (heroToSelect.IsLocked)
            return;

        foreach (var hero in _heroes)
        {
            if(!hero.IsLocked)
                hero.Select(false);
        }

        SelectedHeroView = heroToSelect;
        heroToSelect.Select(true);
    }
}

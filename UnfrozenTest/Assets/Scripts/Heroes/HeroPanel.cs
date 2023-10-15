using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class HeroPanel : MonoBehaviour
{
    //ѕанелька с геро€ми, часть логики можно было бы вынести вне монобеха, но с учетом, что еЄ здесь мало, да и делать надо было быстро, поэтому решил оставить здесь
    [SerializeField] private HeroContainer _heroContainer;
    [SerializeField] private HeroFactory _heroFactory;
    [SerializeField] private Transform _container;

    private List<HeroView> _heroViews = new List<HeroView>();
    public HeroView SelectedHeroView { get; private set; }

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
            foreach (HeroType hero in heroesUnlock)
            {
                HeroView heroView = _heroViews.First(view => view.HeroData.HeroType == hero);
                heroView.Unlock();
            }
        }

        if(addedCharacterPoints.Count() > 0)
        {
            foreach (AddedCharacterPoints characterPoints in addedCharacterPoints)
            {
                if(characterPoints.HeroType == HeroType.CurrentHero)
                {
                    HeroView heroView = _heroViews.First(view => view.IsSelected == true);
                    heroView.UpdateScore(characterPoints.Score);
                }
                else
                {
                    HeroView heroView = _heroViews.First(view => view.HeroData.HeroType == characterPoints.HeroType);
                    heroView.UpdateScore(characterPoints.Score);
                }
            }
        }
    }

    private void Show()
    {
        foreach (var hero in _heroContainer.Heroes)
        {
            HeroView heroView = _heroFactory.Get(hero, _container);
            _heroViews.Add(heroView);
            heroView.OnHeroViewClicked += SelectHero;
        }

        foreach (var heroView in _heroViews)
        {
            if (heroView.HeroData.HeroType == _startHero)
                heroView.Unlock();
        }
    }

    private void SelectHero(HeroView heroView)
    {
        if (heroView.IsLocked)
            return;

        foreach (var heroViewItem in _heroViews)
        {
            if(!heroViewItem.IsLocked)
                heroViewItem.Unselect();
        }

        SelectedHeroView = heroView;
        heroView.Select();
    }
}

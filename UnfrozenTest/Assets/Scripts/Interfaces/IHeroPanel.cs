using System.Collections.Generic;

public interface IHeroPanel
{
    HeroView SelectedHeroView { get; }
    void OnMissionComplete(IEnumerable<HeroType> heroesUnlock, IEnumerable<AddedCharacterPoints> addedCharacterPoints);
}

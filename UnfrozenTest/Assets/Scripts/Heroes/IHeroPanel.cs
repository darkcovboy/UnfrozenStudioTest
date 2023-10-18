using System.Collections.Generic;

public interface IHeroPanel
{
    Hero SelectedHeroView { get; }
    void OnMissionComplete(IEnumerable<HeroType> heroesUnlock, IEnumerable<AddedCharacterPoints> addedCharacterPoints);
}

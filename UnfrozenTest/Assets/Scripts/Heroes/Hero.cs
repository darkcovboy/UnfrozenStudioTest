using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public HeroData HeroData { get; private set; }

    public bool IsLocked { get; private set; }

    public bool IsSelected { get; private set; }

    private int _score;
    private HeroView _heroView;

    public Hero(HeroData heroData, HeroView heroView)
    {
        HeroData = heroData;
        _heroView = heroView;
        _heroView.Initialize(this);
        _score = heroData.Score;
        IsSelected = false;
        IsLocked = true;
        _heroView.UpdateScore(_score);
    }

    public void UpdateScore(int score)
    {
        _score += score;
        _heroView.UpdateScore(_score);
    }

    public void Select(bool isSelected)
    {
        IsSelected = isSelected;

        if(isSelected == true)
            _heroView.Select();
        else
            _heroView.Unselect();
    }

    public void Lock(bool isLocked)
    {
        IsLocked = isLocked;

        if(isLocked == true)
            _heroView.Lock();
        else
            _heroView.Unlock();
    }
}

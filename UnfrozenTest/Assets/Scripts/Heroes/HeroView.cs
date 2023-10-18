using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Наследуемся от IPointerClickHandler, чтобы можно было кликать, ниже Action, который вызывается при клике
public class HeroView : MonoBehaviour, IPointerClickHandler
{
    public event Action<Hero> OnHeroViewClicked;
    //Решил добавить цвета для минимального обозначения что куда
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _backImage;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _lockedColor;

    private int _score;
    private Hero _hero;

    public void Initialize(Hero hero)
    {
        _hero = hero;
        _backImage.color = _defaultColor;
        UpdateName(hero.HeroData.Name);
        Lock();
    }

    public void OnPointerClick(PointerEventData eventData) => OnHeroViewClicked?.Invoke(_hero);

    //Можно было сделать UnityAction какой-нибудь, и вызывать его и менять отображение
    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
    //Методы для выбора и lock/unlock.
    public void Select()
    {
        _backImage.color = _selectedColor;
    }

    public void Unselect()
    {
        _backImage.color = _defaultColor;
    }

    public void Lock()
    {
        _backImage.color = _lockedColor;
    }

    public void Unlock()
    {
        _backImage.color = _defaultColor;
    }

    private void UpdateName(string name)
    {
        _nameText.text = name;
    }
}

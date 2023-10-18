using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//����������� �� IPointerClickHandler, ����� ����� ���� �������, ���� Action, ������� ���������� ��� �����
public class HeroView : MonoBehaviour, IPointerClickHandler
{
    public event Action<Hero> OnHeroViewClicked;
    //����� �������� ����� ��� ������������ ����������� ��� ����
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

    //����� ���� ������� UnityAction �����-������, � �������� ��� � ������ �����������
    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
    //������ ��� ������ � lock/unlock.
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

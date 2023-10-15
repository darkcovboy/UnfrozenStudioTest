using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//����������� �� IPointerClickHandler, ����� ����� ���� �������, ���� Action, ������� ���������� ��� �����
public class HeroView : MonoBehaviour, IPointerClickHandler, IHeroView
{
    public event Action<HeroView> OnHeroViewClicked;
    //����� �������� ����� ��� ������������ ����������� ��� ����
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _backImage;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _lockedColor;

    public HeroData HeroData { get; private set; }

    public bool IsLocked { get; private set; }

    public bool IsSelected { get; private set; }

    private int _score;

    public void Initialize(HeroData heroData)
    {
        HeroData = heroData;
        _score = heroData.Score;
        UpdateScore(heroData.Score);
        UpdateName(heroData.Name);
        IsSelected = false;
        _backImage.color = _defaultColor;
        Lock();
    }

    public void OnPointerClick(PointerEventData eventData) => OnHeroViewClicked?.Invoke(this);

    //����� ���� ������� UnityAction �����-������, � �������� ��� � ������ �����������
    public void UpdateScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }
    //������ ��� ������ � lock/unlock.
    public void Select()
    {
        _backImage.color = _selectedColor;
        IsSelected = true;
    }

    public void Unselect()
    {
        _backImage.color = _defaultColor;
        IsSelected = false;
    }

    public void Lock()
    {
        IsLocked = true;
        _backImage.color = _lockedColor;
    }

    public void Unlock()
    {
        IsLocked = false;
        _backImage.color = _defaultColor;
    }

    private void UpdateName(string name)
    {
        _nameText.text = name;
    }
}

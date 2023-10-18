using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _missionNumber;
    [SerializeField] private Image _image;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _blockedColor;
    [SerializeField] private Color _passedColor;
    [SerializeField] private Color _temproraryLockedColor;
    [SerializeField] private Color _unreacheblaColor;

    public event Action<Mission> OnClick;

    private Mission _mission;

    public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke(_mission);

    public void Initialize(Mission mission)
    {
        _mission = mission;
    }

    public void UpdateText(string index)
    {
        _missionNumber.text = index;
    }

    public void ChangeColor(MissionState missionState)
    {
        switch (missionState)
        {
            case MissionState.Locked:
                SetLocked();
                break;
            case MissionState.Active:
                SetActive();
                break;
            case MissionState.TemporarilyLocked:
                SetTemprorayLocked();
                break;
            case MissionState.Passed:
                SetPassed();
                break;
        }
    }

    public void Block()
    {
        _image.color = _unreacheblaColor;
    }

    private void SetActive()
    {
        _image.color = _activeColor;
    }

    private void SetLocked()
    {
        _image.color = _blockedColor;
    }

    private void SetPassed()
    {
        _image.color = _passedColor;
    }

    private void SetTemprorayLocked()
    {
        _image.color = _temproraryLockedColor;
    }
}

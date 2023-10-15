using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionView : MonoBehaviour, IPointerClickHandler, IMissionView
{
    [SerializeField] private TMP_Text _missionNumber;
    [SerializeField] private Image _image;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _blockedColor;
    [SerializeField] private Color _passedColor;
    [SerializeField] private Color _temproraryLockedColor;
    [SerializeField] private Color _unreacheblaColor;

    public event Action<MissionView> OnClick;

    public MissionData MissionData { get; private set; }
    public MissionState MissionState { get; private set; }

    public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke(this);

    public void Initialize(MissionData missionData, bool isFirstLevel = false)
    {
        MissionData = missionData;
        _image.color = _activeColor;
        _missionNumber.text = missionData.Index.ToString();
        MissionState = MissionState.Locked;

        if (isFirstLevel == true)
            GetNextState();
        else
            SetLocked();
    }

    public void GetNextState()
    {
        switch (MissionState)
        {
            case MissionState.Locked:
                MissionState = MissionState.Active;
                SetActive();
                break;
            case MissionState.Active:
                MissionState = MissionState.TemporarilyLocked;
                SetTemprorayLocked();
                break;
            case MissionState.TemporarilyLocked:
                MissionState = MissionState.Passed;
                SetPassed();
                break;
        }
    }

    public void Lock()
    {
        MissionState = MissionState.Locked;
        SetLocked();
    }

    public void Block()
    {
        SetBLocked();
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

    private void SetBLocked()
    {
        _image.color = _unreacheblaColor;
    }

    private void SetTemprorayLocked()
    {
        _image.color = _temproraryLockedColor;
    }
}

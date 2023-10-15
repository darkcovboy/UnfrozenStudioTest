using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        OffInteractable();
    }

    public void AddListener(UnityAction<MissionData> mission, MissionData missionData)
    {
        _button.onClick.AddListener(() => {
            mission.Invoke(missionData);
        });
    }

    public void AddListener(UnityAction nextState)
    {
        _button.onClick.AddListener(nextState);
    }

    public void RemoveAllListeners()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void OffInteractable()
    {
        _button.interactable = false;
    }

    public void OnInteractable()
    {
        _button.interactable = true;
    }
}

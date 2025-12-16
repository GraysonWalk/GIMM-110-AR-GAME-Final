using UnityEngine;
using System;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    [SerializeField] private GameObject idleUI;


    public event Action<string> OnMiniGameSelected;


    private void Awake()
    {
        Instance = this;
    }


    public void ShowIdleUI()
    {
        idleUI.SetActive(true);
    }


    public void HideIdleUI()
    {
        idleUI.SetActive(false);
    }


    // Hook this to button OnClick events
    public void SelectMiniGame(string gameId)
    {
        OnMiniGameSelected?.Invoke(gameId);
    }
}
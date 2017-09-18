using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public static UiHandler Instance;

    [SerializeField] private GameObject PlayerNameInputPopUp;
    [SerializeField] private InputField PlayerNameInputField;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void ShowPlayerNameInputPopUp(bool state)
    {
        PlayerNameInputPopUp.SetActive(state);
    }

    public void AcceptPlayerName()
    {
        GameDirector.Instance.PlayerName = PlayerNameInputField.text;
        PlayerNameInputPopUp.SetActive(false);

        GameEvent.EndEventById("EnterPlayerName");
    }
}

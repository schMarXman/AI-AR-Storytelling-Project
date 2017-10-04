using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public static UiHandler Instance;

    [SerializeField] private GameObject PlayerNameInputPopUp;
    [SerializeField] private InputField PlayerNameInputField;
    [SerializeField] private RawImage CameraPictureTaken;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

        CameraHandler.Instance.TakePicture();
        CameraHandler.Instance.StopCamera();

        GameEvent.EndEventById("EnterPlayerName");
    }

    public void ShowTakenPicture(bool state)
    {
        CameraPictureTaken.texture = CameraHandler.Instance.PhotoTaken;

        if (state)
        {
            CameraPictureTaken.gameObject.SetActive(true);

            FadeUiElement(CameraPictureTaken.GetComponent<Graphic>(), true, 1.5f);
        }
        else
        {
            FadeUiElement(CameraPictureTaken.GetComponent<Graphic>(), false, 1.5f);

            CameraPictureTaken.gameObject.SetActive(false);
        }


    }

    public void FadeUiElement(Graphic graphic, bool show, float duration)
    {
        graphic.CrossFadeAlpha(show ? 1f : 0f, duration, false);
    }
}

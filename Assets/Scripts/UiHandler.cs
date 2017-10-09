using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public static UiHandler Instance;

    [SerializeField] private GameObject PlayerNameInputPopUp;
    [SerializeField] private GameObject ReadyPopUp;
    [SerializeField] private GameObject PlaceMarkerARAlertLabel;
    [SerializeField] private GameObject BackgroundImage;
    [SerializeField] private GameObject BackgroundFade;
    [SerializeField] private Text PlayerNameText;
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

    public void SetPlaceMarkerLabelActive(bool state)
    {
        PlaceMarkerARAlertLabel.gameObject.SetActive(state);
    }

    public void AcceptPlayerName()
    {
        GameDirector.Instance.PlayerName = PlayerNameInputField.text;
        PlayerNameText.text = GameDirector.Instance.PlayerName = PlayerNameInputField.text;
        PlayerNameInputPopUp.SetActive(false);

        CameraHandler.Instance.TakePicture();
        CameraHandler.Instance.StopCamera();

        if (GameDirector.Instance.PlayerName == "debug")
        {
            DialogueDispatch.Instance.DebugMode = true;
        }

        GameEvent.EndEventById("EnterPlayerName");
    }

    public void SetPlayerNameActive(bool state)
    {
        PlayerNameText.gameObject.SetActive(state);
    }

    public void AcceptReadyPopUp()
    {
        ReadyPopUp.gameObject.SetActive(false);

        GameEvent.EndEventById("AcceptPopUp");
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

    public void SetBackgroundActive(bool state)
    {
        BackgroundImage.SetActive(state);
        BackgroundFade.SetActive(state);
    }

    public void FadeUiElement(Graphic graphic, bool show, float duration)
    {
        graphic.CrossFadeAlpha(show ? 1f : 0f, duration, false);
    }

    public void SetTimeSliderActive(bool state)
    {
        if (ARSceneObjectHelper.Instance != null)
        {
            ARSceneObjectHelper.Instance.TimeSlider.SetActive(state);
        }
    }

    public void SetPersonInfoActive(bool state)
    {
        if (SelectableObject.CurrentlySelectedObject != null)
        {
            SelectableObject.CurrentlySelectedObject.ThisInfoObj.gameObject.SetActive(state);
        }
    }

    //public void SetPlayerName(string name)
    //{
    //}
}

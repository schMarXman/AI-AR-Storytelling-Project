using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;

    public string PlayerName;

    public RawImage WebCam;

    private DeviceOrientation mPreviousOrientation;
    private Vector2 mDefaultResolution;

    private AsyncOperation mLoadingOperation;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        mPreviousOrientation = Input.deviceOrientation;

        mDefaultResolution = new Vector2(Screen.width, Screen.height);

        //mLoadingOperation = SceneManager.LoadSceneAsync("arscene", LoadSceneMode.Additive);
        //mLoadingOperation.allowSceneActivation = false;

    }

    public void UnloadArScene()
    {
        SceneManager.UnloadSceneAsync("arscene");

        UiHandler.Instance.SetBackgroundActive(true);
    }

    public void LoadArScene()
    {
        SceneManager.LoadScene("arscene", LoadSceneMode.Additive);

        UiHandler.Instance.SetBackgroundActive(false);

        GameEvent.EndEventById("SwitchScene");
    }

    public void SetDataParticlesActive(bool state)
    {
        ARSceneObjectHelper.Instance.DataParticles.SetActive(state);
    }

    public void ActivatePersonSelector(bool state)
    {
        ObjectSelector.Instance.Enabled = state;
    }
}

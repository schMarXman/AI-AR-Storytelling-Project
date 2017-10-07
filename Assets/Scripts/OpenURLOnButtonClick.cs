using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenURLOnButtonClick : MonoBehaviour
{
    // HAS TO START WITH HTTP/HTTPS
    public string URL;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenUrl);
    }

    public void OpenUrl()
    {
        Application.OpenURL(URL);
    }
}

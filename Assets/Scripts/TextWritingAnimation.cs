using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWritingAnimation : MonoBehaviour
{
    public float TimeBetweenChars;
    public bool PlayOnStart;

    private Text mText;

    void Start()
    {
        mText = GetComponent<Text>();

        if (PlayOnStart)
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        var chars = mText.text.ToCharArray();

        mText.text = String.Empty;

        for (int i = 0; i < chars.Length; i++)
        {
            mText.text += chars[i];
            yield return new WaitForSeconds(TimeBetweenChars);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDispatch : MonoBehaviour
{
    public static DialogueDispatch Instance;

    public string DialogueFileName;

    public string SeperatorString;

    private string EventRegExString = "<event:.*>";

    public string PlayerNameString;

    public char NewLineReplacementChar;

    public float DisplayTime, TimeBetweenStatements;

    public int MessagesLimit; // 0 = unlimited

    public Text UiMessagePrefab;

    public Transform MessagesPanel;

    private List<string> mStatements;

    private bool mEventHasFinished;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ReadDialogueFile();

        StartCoroutine(DisplayAllLines());
    }

    private void ReadDialogueFile()
    {
        string text;

        if (!Application.isMobilePlatform)
        {
            text = File.ReadAllText(Application.dataPath + @"\" + DialogueFileName);
        }
        else
        {
            text = GameObject.Find("AndroidDialogueHolder").GetComponent<AndroidDialogueHolder>().Dialogue;
        }

        text = text.Replace(Environment.NewLine, NewLineReplacementChar.ToString());

        mStatements = text.Split(SeperatorString.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

        // Clean up strings
        for (int i = 0; i < mStatements.Count; i++)
        {
            // starts with new line
            if (mStatements[i].StartsWith(NewLineReplacementChar.ToString()))
            {
                mStatements[i] = mStatements[i].TrimStart(NewLineReplacementChar);
            }

            // ends with new line
            if (mStatements[i].EndsWith(NewLineReplacementChar.ToString()))
            {
                mStatements[i] = mStatements[i].TrimEnd(NewLineReplacementChar);
            }

            if (mStatements[i] == String.Empty)
            {
                mStatements.RemoveAt(i);
                i--;
            }

            Debug.Log(mStatements[i]);
        }
    }

    private IEnumerator DisplayAllLines()
    {
        for (int i = 0; i < mStatements.Count; i++)
        {
            mEventHasFinished = false;

            var statement = mStatements[i];

            statement = ReplacePlayerName(statement);

            var eventIdMatch = StatementHasEvent(statement);

            statement = statement.Remove(eventIdMatch.Index, eventIdMatch.Length);

            yield return StartCoroutine(DisplayTextAnimated(DisplayTime, statement, true));

            if (eventIdMatch.Value != String.Empty)
            {
                GameEvent.TriggerEventById(eventIdMatch.Value);

                while (mEventHasFinished == false)
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(TimeBetweenStatements);
        }
    }

    private Match StatementHasEvent(string statement)
    {
        return Regex.Match(statement, EventRegExString);
    }

    private string ReplacePlayerName(string statement)
    {
        if (statement.Contains(PlayerNameString) && GameDirector.Instance.PlayerName != String.Empty)
        {
            return statement.Replace(PlayerNameString, GameDirector.Instance.PlayerName);
        }

        return statement;
    }

    private IEnumerator DisplayTextAnimated(float time, string text, bool fixedTime = false)
    {
        var instantiatedText = InstantiateNewMessage();

        var chars = text.ToCharArray();

        float waitingTime;

        if (fixedTime)
        {
            waitingTime = time;
        }
        else
        {
            waitingTime = time / chars.Length;
        }

        string displayText = string.Empty;

        for (int i = 0; i < chars.Length; i++)
        {
            var currentChar = chars[i];

            if (currentChar != NewLineReplacementChar)
            {
                displayText += chars[i];

                instantiatedText.text = displayText;
            }
            else
            {
                displayText = String.Empty;

                instantiatedText = InstantiateNewMessage();
            }

            yield return new WaitForSeconds(waitingTime);
        }
    }

    private Text InstantiateNewMessage()
    {
        var instantiatedText = Instantiate(UiMessagePrefab.gameObject).GetComponent<Text>();
        instantiatedText.transform.SetParent(MessagesPanel);

        if (MessagesLimit > 0)
        {
            if (MessagesPanel.childCount > MessagesLimit)
            {
                Destroy(MessagesPanel.GetChild(0).gameObject);
            }
        }

        return instantiatedText;
    }

    public void ContinueDialogueAfterEvent()
    {
        mEventHasFinished = true;
    }
}

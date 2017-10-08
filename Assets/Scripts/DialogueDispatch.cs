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

    public char CommentChar;

    private string EventRegExString = "<event:.*>";

    public string PlayerNameString;

    public string SelectedPersonString;

    public char NewLineReplacementChar;

    public float DisplayTime, TimeBetweenStatements;

    public int MessagesLimit; // 0 = unlimited

    public int FadedMessagesAmount;

    public Text UiMessagePrefab;

    public Transform MessagesPanel;

    private List<string> mStatements;

    private bool mEventHasFinished;

    private const int mMaxCharAmountPerStatement = 37; // font size 20 with ui scaled by resolution with "> "

    public bool DebugMode;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Fill with empty messages, so they get properly faded
        if (FadedMessagesAmount > 0)
        {
            for (int i = 0; i < MessagesLimit; i++)
            {
                InstantiateNewMessage(false);
            }
        }

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
            var statement = mStatements[i];

            // Remove comments
            if (statement.StartsWith(CommentChar.ToString()) || statement.StartsWith(string.Empty + NewLineReplacementChar + CommentChar))
            {
                mStatements.RemoveAt(i);
                i--;
                continue;
            }

            // starts with new line
            if (statement.StartsWith(NewLineReplacementChar.ToString()))
            {
                mStatements[i] = statement.TrimStart(NewLineReplacementChar);
            }

            // ends with new line
            if (statement.EndsWith(NewLineReplacementChar.ToString()))
            {
                mStatements[i] = statement.TrimEnd(NewLineReplacementChar);
            }

            if (statement == String.Empty)
            {
                mStatements.RemoveAt(i);
                i--;
                if (i < 0)
                {
                    i = 0;
                }
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
            statement = ReplaceSelectedPersonString(statement);

            var eventIdMatch = StatementHasEvent(statement);

            statement = statement.Remove(eventIdMatch.Index, eventIdMatch.Length);

            statement = FindLastSpaceBeforeCharLimitReplaceWithBreak(statement);
            //CalculateMessageSize(statement);

            if (!DebugMode)
            {
                yield return StartCoroutine(DisplayTextAnimated(DisplayTime, statement, true));
            }
            else
            {
                yield return StartCoroutine(DisplayTextAnimated(0.001f, statement, true));
            }

            if (eventIdMatch.Value != String.Empty)
            {
                GameEvent.TriggerEventById(eventIdMatch.Value);

                while (mEventHasFinished == false)
                {
                    yield return null;
                }
            }

            if (!DebugMode)
            {
                yield return new WaitForSeconds(TimeBetweenStatements);

            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
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

    private string ReplaceSelectedPersonString(string statement)
    {
        if (statement.Contains(SelectedPersonString) && SelectableObject.CurrentlySelectedObject != null)
        {
            string replacement = SelectableObject.CurrentlySelectedObject.IsMale ? "Herr " : "Frau ";
            replacement += SelectableObject.CurrentlySelectedObject.Person.LastName;

            return statement.Replace(SelectedPersonString, replacement);
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

        string displayText = "> ";

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

    private Text InstantiateNewMessage(bool fadeOutMsgs = true)
    {
        var instantiatedText = Instantiate(UiMessagePrefab.gameObject).GetComponent<Text>();
        instantiatedText.transform.SetParent(MessagesPanel);
        instantiatedText.transform.localScale = Vector3.one;

        if (MessagesLimit > 0)
        {
            if (MessagesPanel.childCount > MessagesLimit)
            {
                Destroy(MessagesPanel.GetChild(0).gameObject);
            }
        }

        if (fadeOutMsgs)
        {
            FadeOutMessages();
        }
        return instantiatedText;
    }

    private string FindLastSpaceBeforeCharLimitReplaceWithBreak(string statement)
    {
        List<string> originalLineBreaks = new List<string>();

        if (statement.Contains(NewLineReplacementChar))
        {
            while (statement.Contains(NewLineReplacementChar))
            {
                var tillBreak = statement.Substring(0, statement.IndexOf(NewLineReplacementChar) + 1);
                originalLineBreaks.Add(tillBreak);
                statement = statement.Replace(tillBreak, string.Empty);
            }
        }

        originalLineBreaks.Add(statement);

        string finishedStatement = String.Empty;

        for (int i = 0; i < originalLineBreaks.Count; i++)
        {
            string newStatement = String.Empty;

            var splitString = originalLineBreaks[i];

            var spaceSplits = splitString.Split(' ');

            for (int j = 0; j < spaceSplits.Length; j++)
            {
                var word = spaceSplits[j];

                var newAddition = newStatement == String.Empty ? newStatement + word : newStatement + " " + word;

                if (newAddition.Length > mMaxCharAmountPerStatement)
                {
                    newStatement += NewLineReplacementChar;
                    finishedStatement += newStatement;
                    newStatement = word;
                }
                else
                {
                    newStatement = newAddition;
                }
            }

            finishedStatement += newStatement;
            //if (splitString.Length > mMaxCharAmountPerStatement)
            //{
            //    var tillCharLimit = splitString.Substring(0, mMaxCharAmountPerStatement);
            //    splitString = splitString.Remove(0, mMaxCharAmountPerStatement);
            //    int lastSpaceIndex = tillCharLimit.LastIndexOf(" ");
            //    tillCharLimit = tillCharLimit.Remove(lastSpaceIndex, 1);
            //    tillCharLimit = tillCharLimit.Insert(lastSpaceIndex, NewLineReplacementChar.ToString());

            //}
        }

        return finishedStatement;
    }

    private void FadeOutMessages()
    {
        if (MessagesPanel.childCount >= MessagesLimit - FadedMessagesAmount)
        {
            float fadeStep = 1f / (FadedMessagesAmount + 1);

            for (int i = 1; i <= FadedMessagesAmount; i++)
            {
                var textMsg = MessagesPanel.GetChild(i).GetComponent<Text>();

                var col = textMsg.color;
                col.a = i * fadeStep;
                textMsg.color = col;
            }
        }
    }

    public void ContinueDialogueAfterEvent()
    {
        mEventHasFinished = true;
    }

    private void CalculateMessageSize(string statement)
    {
        var chars = statement.ToCharArray();
        var font = UiMessagePrefab.font;

        int length = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            CharacterInfo cInfo;
            font.GetCharacterInfo(chars[i], out cInfo);

            length += cInfo.advance;
        }

        Debug.Log("Statement length: " + length + "; Screen width: " + Screen.width);
    }
}

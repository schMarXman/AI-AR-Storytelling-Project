using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public static List<GameEvent> GameEvents = new List<GameEvent>();

    public string EventId;

    public UnityEvent OnEventTriggered;

    public bool EndEventImmediately;

    public UnityEvent OnEventEnded;

    [HideInInspector]
    public bool IsActive { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(transform.parent);

        GameEvents.Add(this);
        OnEventEnded.AddListener(ContinueDialogue);
    }

    public static void TriggerEventById(string id)
    {
        var gameEvent = GetEventById(id);

        gameEvent.Trigger();
    }

    public static void EndEventById(string id, bool onlyEndIfActive = false)
    {
        var gameEvent = GetEventById(id);

        gameEvent.End(onlyEndIfActive);
    }

    public static GameEvent GetEventById(string id)
    {
        if (id.Contains("<event:"))
        {
            id = id.Replace("<event:", string.Empty);
            id = id.Replace(">", string.Empty);
        }

        foreach (var gameEvent in GameEvents)
        {
            if (gameEvent.EventId == id)
            {
                return gameEvent;
            }
        }

        Debug.LogErrorFormat("Event with id:{0} does not exist!");
        return null;
    }

    public void Trigger()
    {
        IsActive = true;

        OnEventTriggered.Invoke();

        if (EndEventImmediately)
        {
            End();
        }
    }

    public void End(bool onlyEndIfActive = false)
    {
        if (onlyEndIfActive && !IsActive)
        {
            return;
        }

        OnEventEnded.Invoke();
        IsActive = false;
    }

    private void ContinueDialogue()
    {
        DialogueDispatch.Instance.ContinueDialogueAfterEvent();
    }
}

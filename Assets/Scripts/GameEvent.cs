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

    public UnityEvent OnEventEnded;

    void Awake()
    {
        GameEvents.Add(this);
        OnEventEnded.AddListener(ContinueDialogue);
    }

    public static void TriggerEventById(string id)
    {
        var gameEvent = GetEventById(id);

        gameEvent.Trigger();
    }

    public static void EndEventById(string id)
    {
        var gameEvent = GetEventById(id);

        gameEvent.End();
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
        OnEventTriggered.Invoke();
    }

    public void End()
    {
        OnEventEnded.Invoke();
    }

    private void ContinueDialogue()
    {
        DialogueDispatch.Instance.ContinueDialogueAfterEvent();
    }
}

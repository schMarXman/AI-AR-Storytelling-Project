using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AndroidDialogueHolder : MonoBehaviour
{
    public static AndroidDialogueHolder Instance;

    public string DialogueFileName = "Dialogues.txt";

    public string Dialogue;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void Populate()
    {
        Dialogue = File.ReadAllText(Application.dataPath + @"\" + DialogueFileName);
        Dialogue = Dialogue.Replace(Environment.NewLine, "@");
        Debug.Log(Dialogue);
    }
}

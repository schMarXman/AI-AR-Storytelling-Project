#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.SceneManagement;
using UnityEngine;

class OnBuildingAndroid : IPreprocessBuild
{
    public int callbackOrder { get; private set; }

    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        if (target == BuildTarget.Android)
        {
            Debug.Log("Loading dialogue strings for android!");
            GameObject.Find("AndroidDialogueHolder").GetComponent<AndroidDialogueHolder>().Populate();
        }
    }
}
#endif
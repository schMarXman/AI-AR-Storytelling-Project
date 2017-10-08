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
            //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            //EditorSceneManager.LoadScene("dialoguestart");
            Debug.Log("Loading dialogue strings for android!");
            GameObject.Find("AndroidDialogueHolder").GetComponent<AndroidDialogueHolder>().Populate();
            GameObject.Find("DialogueDispatch").GetComponent<DialogueDispatch>().DebugMode = false;
        }
    }
}
#endif
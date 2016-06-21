using UnityEngine;
using System.Collections;

public class SceneOpener : MonoBehaviour {

    public string sceneName;

    public void ChangeLevel()
    {
        Application.LoadLevel(sceneName);
    }
}

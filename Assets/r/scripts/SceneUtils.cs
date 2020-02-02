using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    public void LoadSceneByName(string id){
        var sc = SceneManager.GetSceneByName(id);
        if(sc != null)
            SceneManager.LoadScene(id);
        else
            Debug.LogWarning("Attempting to load scene that does not exist by (name)...");
    }

    public void LoadSceneByIndex(int index){
        var sc = SceneManager.GetSceneByBuildIndex(index);
        if(sc != null)
            SceneManager.LoadScene(index);
        else
            Debug.LogWarning("Attempting to load scene that does not exist by (index)...");
    }
}

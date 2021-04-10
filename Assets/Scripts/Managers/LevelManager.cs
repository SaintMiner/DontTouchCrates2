using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    string _currentLevelName = string.Empty;    

    public IEnumerator LoadLevel(string levelName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (asyncOperation == null)
        {
            Debug.LogError($"[LevelManager] Unable to load level {levelName}");
            yield break;
        }        

        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            // m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                // m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
        
        _currentLevelName = levelName;
        Debug.Log(_currentLevelName);        
    }
    
    private void UnloadLevel(string levelName)
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(levelName);

        if (asyncOperation == null)
        {
            Debug.LogError($"[GameManager] Unable to unload level {levelName}");
            return;
        }
        Debug.Log(_currentLevelName);
    }

    public static IEnumerator ChangeLevel(string levelName)
    {
        // if (Instance._currentLevelName != string.Empty)
        // {
        //     Instance.UnloadLevel(Instance._currentLevelName);
        // }

        Instance.StartCoroutine(Instance.LoadLevel(levelName));
        yield return 0;
    }



    
}

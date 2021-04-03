using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    string _currentLevelName = string.Empty;    

    public void LoadLevel(string levelName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (asyncOperation == null)
        {
            Debug.LogError($"[LevelManager] Unable to load level {levelName}");
            return;
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

    public static void ChangeLevel(string levelName)
    {
        if (Instance._currentLevelName != string.Empty)
        {
            Instance.UnloadLevel(Instance._currentLevelName);
        }

        Instance.LoadLevel(levelName);
    }



    
}

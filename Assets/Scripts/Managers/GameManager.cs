using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // List of GameStates
    public enum GameState
    {
        PREGAME,
        LOADING,
        RUNNING,
        PAUSED
    }

    [SerializeField] List<GameObject> systemManagers;

    List<GameObject> _intancedSystemManagers;
    GameState _currentGameState;

    private void Start()
    {        
        _intancedSystemManagers = new List<GameObject>();

        InstatiateSystemManagers();
        StartGame();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_currentGameState != GameState.PAUSED)
            {
                _currentGameState = GameState.PAUSED;
                Time.timeScale = 0;
            }
            else
            {
                _currentGameState = GameState.RUNNING;
                Time.timeScale = 1;
            }
        }
    }

    private void InstatiateSystemManagers()
    {        
        systemManagers.ForEach(delegate (GameObject manager)
        {
            _intancedSystemManagers.Add(Instantiate(manager));
        });
    }

    private void StartGame()
    {
        _currentGameState = GameState.PREGAME;
        LevelManager.ChangeLevel("MainScene");
    }

}

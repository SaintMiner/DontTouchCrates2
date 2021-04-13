using System;
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

    [SerializeField] private List<GameObject> systemManagers;

    private List<GameObject> _intancedSystemManagers;
    private GameState _currentGameState;
    private int _challengeCount;
    private int _points;

    public static event Action<Pickup> OnPlayerPickupTrigger;
    public static event Action OnPlayerCrateTouch;

    private void Start()
    {        
        _intancedSystemManagers = new List<GameObject>();

        OnPlayerPickupTrigger += GameManager_OnPlayerPickupTrigger;
        ChallengeManager.OnChallegeComplete += ChallengeManager_OnChallengeEnded;

        InstatiateSystemManagers();
        StartGame();
    }

    private void ChallengeManager_OnChallengeEnded(ChallengePickup.ChallengeType obj)
    {
        int gettingPoints = 0;
        switch(obj) {
            case ChallengePickup.ChallengeType.CRATE_RAIN:
                gettingPoints = 100;
                break;
            case ChallengePickup.ChallengeType.LAUCHING_CRATES:
                gettingPoints = 150;
                break;
        }
        _points += gettingPoints * _challengeCount;
        Debug.Log(_points);
    }

    private void GameManager_OnPlayerPickupTrigger(Pickup obj)
    {
        _challengeCount++;
    }

    public void GameManager_OnPlayerLose() {
        
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
        _points = _challengeCount = 0;
        _currentGameState = GameState.PREGAME;
        StartCoroutine(LevelManager.ChangeLevel("MainScene"));
    }

    public static void TriggerPlayerPickup(Pickup pickup)
    {
        OnPlayerPickupTrigger?.Invoke(pickup);
    }

    public static void TriggerPlayerCrateTouch()
    {
        OnPlayerCrateTouch?.Invoke();
    }

}

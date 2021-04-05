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
        Debug.Log(obj);
    }

    private void GameManager_OnPlayerPickupTrigger(Pickup obj)
    {
        _challengeCount++;
        Debug.Log(_challengeCount);
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
        _challengeCount = 0;
        _currentGameState = GameState.PREGAME;
        LevelManager.ChangeLevel("MainScene");
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

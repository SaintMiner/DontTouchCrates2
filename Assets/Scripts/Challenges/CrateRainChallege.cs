using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateRainChallege : Challenge
{
    [SerializeField] private GameObject _cratePrefab;
    
    private int _counter;

    protected override void Awake()
    {
        _counter = 0;
        _interval = 0.5f;        
    }

    protected override void ChallengeAction()
    {
        Debug.Log("Crate Rain");
        _counter++;        
        Instantiate(_cratePrefab, ChallengeManager.GenerateSpawnPosition(), _cratePrefab.gameObject.transform.rotation);
    }

    protected override bool ChallengeCondition()
    {
        return _counter <= 10;
    }
}

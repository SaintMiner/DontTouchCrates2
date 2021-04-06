using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateRainChallege : Challenge
{
    [SerializeField] private GameObject _cratePrefab;
    [SerializeField] private int _baseCountToComplete = 10;
    private int _countToComplete;
    private int _counter;

    protected override void Awake()
    {
        _countToComplete = _baseCountToComplete + ChallengeManager.Instance.CompletedChallengeCount;
        _challengeType = ChallengePickup.ChallengeType.FALLING_CRATES;
        _counter = 0;
        _interval = 5f / _countToComplete;
    }

    protected override void ChallengeAction()
    {
        _counter++;
        ChallengeManager.Instance.SpawnCrate();
    }

    protected override bool ChallengeCondition()
    {
        return _counter < _countToComplete;
    }
}

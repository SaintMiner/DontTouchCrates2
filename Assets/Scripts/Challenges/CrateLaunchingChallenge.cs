using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateLaunchingChallenge : Challenge
{
    [SerializeField] private int _baseCountToComplete = 10;
    private int _countToComplete;
    private int _counter;

    protected override void Awake()
    {
        _countToComplete = _baseCountToComplete + ChallengeManager.Instance.CompletedChallengeCount;
        _challengeType = ChallengePickup.ChallengeType.LAUCHING_CRATES;
        _counter = 0;
        _points = 100;
        _interval = 1;
    }

    protected override void ChallengeAction()
    {
        _counter++;
        ChallengeManager.Instance.SpawnCrate(_challengeType);
    }

    /* TODO:
     * fix: rapid condition challenge end
    */
    protected override bool ChallengeCondition()
    {
        return _counter < _countToComplete;
    }
}

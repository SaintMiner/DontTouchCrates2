using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengePickup : Pickup
{
    [SerializeField] private Renderer _challengeRenderer;
    [SerializeField] private ChallengeType _challengeType;
    public enum ChallengeType
    {
        FALLING_CRATES,
        LAUCHING_CRATES
    }
    private void Start()
    {
        GameManager.OnPlayerPickupTrigger += GameManager_OnPlayerPickupTrigger;
        ChallengeManager.OnChallegeComplete += ChallengeManager_OnChallengeEnded;
    }

    private void ChallengeManager_OnChallengeEnded(ChallengeType obj)
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _challengeType = (ChallengeType)Random.Range(0, System.Enum.GetValues(typeof(ChallengeType)).Length);
        SetChallengeColor();        
    }


    private void GameManager_OnPlayerPickupTrigger(Pickup obj)
    {
        gameObject.SetActive(false);
    }

    private void SetChallengeColor()
    {        
        switch (_challengeType)
        {
            case ChallengeType.FALLING_CRATES:
                _challengeRenderer.material.color = Color.cyan;                
                break;

            case ChallengeType.LAUCHING_CRATES:
                _challengeRenderer.material.color = Color.green;                
                break;
        }                
    }

    protected override void TriggeredByPlayer()
    {
        Debug.Log("Challenge pickUp triggered by player");
        base.TriggeredByPlayer();
        ChallengeManager.Instance.ActivateChallenge(_challengeType);
    }
}

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
    
    private void OnEnable()
    {
        _challengeType = (ChallengeType)Random.Range(0, System.Enum.GetValues(typeof(ChallengeType)).Length);
        SetChallengeColor();        
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

    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        ChallengeManager.Instance.ActivateChallenge(_challengeType);
    }
}

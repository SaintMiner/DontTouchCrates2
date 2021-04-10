using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Singleton<Platform>
{
    [SerializeField] private List<GameObject> _parts;

    protected override void Awake()
    {
        _persistent = false;

        foreach (GameObject child in _parts)
        {
            child.tag = gameObject.tag;
        }

        base.Awake();
    }

    public List<GameObject> GetPlatformActiveParts()
    {
        return _parts;
    }

    public static GameObject RandomActivePart()
    {
        int activePartCount = Instance.GetPlatformActiveParts().Count;
        int randomIndex = Random.Range(0, activePartCount);
        GameObject part = Instance.GetPlatformActiveParts()[randomIndex];
        
        return part;
    }
}

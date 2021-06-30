using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 offset;

    Transform ownTransform;

    Vector3 previousPosition;

    // Start is called before the first frame update
    void Awake()
    {
        ownTransform = transform;

        if(target != null)
            previousPosition = target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            ownTransform.position = target.position + offset;
            
            previousPosition = target.position;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        previousPosition = target.position;
    }

    public void UpdateOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
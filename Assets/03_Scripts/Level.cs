using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    Transform finishLineTransform;

    public float GetFinishLineZ()
    {
        return finishLineTransform.position.z;
    }
}

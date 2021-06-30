using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressionSlider : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Slider distanceSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitLevelDistance(float finishZPosition)
    {
        distanceSlider.maxValue = finishZPosition;
    }

    void Update()
    {
        distanceSlider.value = playerTransform.position.z;
    }
}

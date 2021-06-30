using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowntownGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject prefabCube;

    [SerializeField]
    float cubeSize = 10.0f;

    [SerializeField]
    float maxHeightDifference = 100.0f;

    [SerializeField]
    int sideSurface = 50;

    [SerializeField]
    int lengthSurface = 100;

    // Start is called before the first frame update
    void Awake()
    {
        Transform ownTransform = transform;

        bool isBonus = PlayerPrefs.HasKey("BonusLevel");

        int halfSurfaceLeft = sideSurface / 2 - 1;
        int halfSurfaceRight = sideSurface / 2 + 1;

        // Fill left part of the field
        for (int i = 0; i < halfSurfaceLeft; i++)
        {
            for (int j = 0; j < lengthSurface; j++)
            {
                if(i%2 == 0 && j%2 == 0)
                    InstantiateDecor(i, j);
            }
        }

        // Fill right part of the field
        for (int i = halfSurfaceRight; i < sideSurface; i++)
        {
            for (int j = 0; j < lengthSurface; j++)
            {
                if (i % 2 == 0 && j % 2 == 0)
                    InstantiateDecor(i, j);
            }
        }
    }

    private void InstantiateDecor(int i, int j)
    {
        GameObject cube = Instantiate(prefabCube);

        cube.transform.localScale = new Vector3(cubeSize, cubeSize + UnityEngine.Random.Range(0.0f, maxHeightDifference), cubeSize);

        cube.transform.SetParent(transform);

        cube.transform.localPosition = new Vector3(-cubeSize * (float)sideSurface / 2.0f + i * cubeSize, 0.0f, -cubeSize * (float)lengthSurface / 2.0f + j * cubeSize);
    }
}

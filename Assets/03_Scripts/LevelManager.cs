using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> levels;
    
    // Used to instantiate levels. Can also be used later to create levels instead of instantiating existing ones.
    public Level Init(int level)
    {
        int index = level % levels.Count;

        GameObject levelObject = Instantiate(levels[index]);

        return levelObject.GetComponent<Level>();
    }
}

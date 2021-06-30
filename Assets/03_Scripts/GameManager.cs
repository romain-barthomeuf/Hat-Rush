using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    LevelProgressionSlider levelProgressionSlider;

    [SerializeField]
    GameObject gamePanel;

    [SerializeField]
    GameObject endGamePanel;

    int currentLevel = 0;

    Level level;

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
        gamePanel.SetActive(false);

        int currentLevel = PlayerPrefs.GetInt("Level", 0);

        level = levelManager.Init(currentLevel);

        levelProgressionSlider.InitLevelDistance(level.GetFinishLineZ());

        EventManager.Instance.AddListener<GameFinishedEvent>(OnGameFinished);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gamePanel.SetActive(true);
        EventManager.Instance.QueueEvent(new GameStartedEvent());
    }

    public void OnGameFinished(GameFinishedEvent e)
    {
        gamePanel.SetActive(false);

        // If the player won, increase his current level
        if(e.playerWon)
        {
            currentLevel++;

            PlayerPrefs.SetInt("Level", currentLevel);
            PlayerPrefs.Save();
        }

        StartCoroutine(EndGameDelay());
    }

    IEnumerator EndGameDelay()
    {
        yield return new WaitForSeconds(1.0f);
        
        endGamePanel.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
}

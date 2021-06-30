using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishedEvent : GameEvent
{
    public bool playerWon = false;

    public bool playerLostToObstacle = false;

    public GameFinishedEvent(bool playerWon, bool playerLostToObstacle = false)
    {
        this.playerWon = playerWon;

        this.playerLostToObstacle = playerLostToObstacle;
    }
}

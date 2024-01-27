using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MiniGameBase MiniGame;

    private void Start()
    {
        MiniGame = new game1();
        MiniGame.SetupGame();
    }

    void Update()
    {
        MiniGame.UpdateGame();
    }

    void SwitchMiniGame(int gameID)
    {
        switch(gameID)
        {
            case 0:
                break;
        }
    }
}

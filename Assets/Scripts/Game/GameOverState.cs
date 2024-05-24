using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverState : IState
{
    private Game game;
    
    
    public GameOverState(Game game)
    {
        this.game = game;
    }

    
    #region IState

    public void Enter()
    {
        game.CurrentPlayer = Player.None;
        game.gameOverMenu.SetActive(true);
    }

    public void Update()
    {
    }

    public void Exit()
    {
        
    }

    #endregion
}
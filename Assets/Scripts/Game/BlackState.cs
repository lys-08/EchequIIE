using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackState : IState
{
    private Game game;
    
    
    public BlackState(Game game)
    {
        this.game = game;
    }

    
    #region IState

    public void Enter()
    {
        game.CurrentPlayer = Player.Black;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        
    }

    #endregion
}
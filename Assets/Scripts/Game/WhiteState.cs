using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WhiteState : IState
{
    private Game game;
    
    
    public WhiteState(Game game)
    {
        this.game = game;
    }

    
    #region IState

    public void Enter()
    {
        game.CurrentPlayer = Player.White;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        
    }

    #endregion
}
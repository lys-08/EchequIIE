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
        Debug.Log("Black");
        game.CurrentPlayer = Player.Black;
    }

    public void Update()
    {
        //game.MakeMove(move);
        //game.GamestateMachine.TransitionTo(game.GamestateMachine.whiteState);
    }

    public void Exit()
    {
        
    }

    #endregion
}
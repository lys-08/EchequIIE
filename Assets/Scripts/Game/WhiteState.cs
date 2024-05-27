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
        Debug.Log($"white : {game.Board.IsInCheck(Player.White)}");
        Debug.Log($"black : {game.Board.IsInCheck(Player.Black)}");
        
        if (game.Board.IsInCheck(game.CurrentPlayer))
        {
            game.StartCoroutine(game.PrintCheckMenu());
        }
    }

    public void Update()
    {
    }

    public void Exit()
    {
        
    }

    #endregion
}
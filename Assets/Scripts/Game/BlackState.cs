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
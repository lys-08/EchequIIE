using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Move //: MonoBehaviour
{
    public abstract MoveType Type { get; }
    public abstract Position FromPos { get; } // Position where the piece move from
    public abstract Position ToPos { get; } // Position where the piece move to

    /**
     * Execute itself on the board
     * -> like the command pattern
     */
    public abstract void Execute(Board board);

    /**
     * Return true if the move does not leave the current player's king in check
     *
     * TODO : check
     */
    public virtual bool IsLegal(Board board)
    {
        Game game = GameObject.FindObjectOfType<Game>();
        Player player = board[FromPos].GetComponentInChildren<Piece>().Color;

        Board boardCopy = board.Copy();
        game.Board = boardCopy;
        Execute(boardCopy);
        bool isLegal = !boardCopy.IsInCheck(player);

        game.Board = board;
        GameObject.Destroy(boardCopy);

        return isLegal;
    }
}

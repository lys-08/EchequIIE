using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Move //: MonoBehaviour
{
    public Game game = GameObject.FindObjectOfType<Game>();
    public abstract MoveType Type { get; }
    public abstract Position FromPos { get; } // Position where the piece move from
    public abstract Position ToPos { get; } // Position where the piece move to

    /**
     * Execute itself on the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public abstract bool Execute(Board board);

    /**
     * Execute itself on the copy of the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public abstract void ExecuteCopy(Piece[,] board);

    /**
     * Return true if the move does not leave the current player's king in check
     *
     */
    public virtual bool IsLegal(Board board)
    {
        Player player = board[FromPos].GetComponentInChildren<Piece>().Color;

        Piece[,] boardCopy = board.Copy();
        ExecuteCopy(boardCopy);
        
        return !board.IsInCheckCopy(boardCopy, player);
    }
}

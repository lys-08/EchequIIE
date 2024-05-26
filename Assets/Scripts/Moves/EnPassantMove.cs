using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnPassantMove : Move
{
    public override MoveType Type => MoveType.EnPassant;
    public override Position FromPos { get; }
    public override Position ToPos { get; }
    private Position capturePos; // Position of the captured pawn

    public EnPassantMove(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
        capturePos = new Position(fromPos.Row, toPos.Column);
    }

    /**
     * Execute itself on the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override bool Execute(Board board)
    {
        new NormalMove(FromPos, ToPos).Execute(board);
        GameObject.Destroy(board[capturePos].GetComponentInChildren<Piece>().gameObject);
        
        return true; // always move a pawn
    }
    
    /**
     * Execute itself on the copy of the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override void ExecuteCopy(Piece[,] board)
    {
        new NormalMove(FromPos, ToPos).ExecuteCopy(board);
        board[capturePos.Row, capturePos.Column] = null;
    }
}

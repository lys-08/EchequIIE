using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoublePawnMove : Move
{
    public override MoveType Type => MoveType.DoublePawn;
    public override Position FromPos { get; }
    public override Position ToPos { get; }
    private Position skippedPosition; // En Passant square : the case skipped

    public DoublePawnMove(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
        skippedPosition = new Position((fromPos.Row + toPos.Row) / 2, fromPos.Column);
    }

    /**
     * Execute itself on the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override bool Execute(Board board)
    {
        Player player = board[FromPos].GetComponentInChildren<Piece>().Color;
        board.SetPawnSkipPosition(player, skippedPosition);
        new NormalMove(FromPos, ToPos).Execute(board);
        
        return true; // always move a pawn
    }
    
    /**
     * Execute itself on the copy of the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override void ExecuteCopy(Piece[,] board)
    {
        new NormalMove(FromPos, ToPos).ExecuteCopy(board);
    }
}

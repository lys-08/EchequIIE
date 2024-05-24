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
     * Execute itself on the board
     * -> like the command pattern
     */
    public override void Execute(Board board)
    {
        Player player = board[FromPos].GetComponentInChildren<Piece>().Color;
        board.SetPawnSkipPosition(player, skippedPosition);
        new NormalMove(FromPos, ToPos).Execute(board);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnPassantMove : Move
{
    public override MoveType Type => MoveType.EnPassant;
    public override Position FromPos { get; }
    public override Position ToPos { get; }
    private Position capturePos;

    public EnPassantMove(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
        capturePos = new Position(fromPos.Row, toPos.Column);
    }

    /**
     * Execute itself on the board
     * -> like the command pattern
     */
    public override void Execute(Board board)
    {
        new NormalMove(FromPos, ToPos).Execute(board);
        GameObject.Destroy(board[capturePos].GetComponentInChildren<Piece>().gameObject);
    }
}

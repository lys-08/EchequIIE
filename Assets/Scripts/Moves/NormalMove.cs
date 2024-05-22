using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMove : Move
{
    public override MoveType Type => MoveType.Normal;
    
    public override Position FromPos { get; }
    public override Position ToPos { get; }

    public NormalMove(Position fromPos, Position toPos)
    {
        FromPos = fromPos;
        ToPos = toPos;
    }

    public override void Execute(Board board)
    {
        GameObject piece = board[FromPos].GetComponentInChildren<Piece>().gameObject;
        piece.transform.parent = board[ToPos].transform;
        //board[FromPos] = null;
    }
}

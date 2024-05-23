using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        GameObject.Destroy(board[ToPos].GetComponentInChildren<Piece>().GameObject());
        
        GameObject piece = board[FromPos].GetComponentInChildren<Piece>().gameObject;
        board[FromPos].GetComponentInChildren<Piece>().HasMoved = true;
        piece.transform.parent = board[ToPos].transform;
        piece.transform.position = 0.02f * new Vector3(1.25f * ToPos.Column, 0.05f, 1.25f * ToPos.Row);
    }
}

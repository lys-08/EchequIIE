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

    /**
     * Execute itself on the board
     * -> like the command pattern
     */
    public override void Execute(Board board)
    {
        GameObject.Destroy(board[ToPos].GetComponentInChildren<Piece>().GameObject());
        
        GameObject piece = board[FromPos].GetComponentInChildren<Piece>().gameObject;
        board[FromPos].GetComponentInChildren<Piece>().HasMoved = true;
        piece.transform.parent = board[ToPos].transform;
        piece.transform.localScale = Vector3.one;
        piece.transform.position = board[ToPos].transform.position;//Vector3.zero;//0.02f * new Vector3(1.25f * ToPos.Column, 0.05f, 1.25f * ToPos.Row);
        Debug.Log(piece.transform.position);
        Debug.Log(piece.transform.parent.gameObject);
    }
}

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
     * Execute itself on the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override bool Execute(Board board)
    {
        bool capture = false;
        if (!board.IsEmpty(ToPos))
        {
            capture = true;
            GameObject.Destroy(board[ToPos].GetComponentInChildren<Piece>().gameObject);
        }

        Piece piece = board[FromPos].GetComponentInChildren<Piece>();
        GameObject pieceGm = piece.gameObject;
        piece.HasMoved = true;
        pieceGm.transform.parent = board[ToPos].transform;
        pieceGm.transform.localScale = Vector3.one;
        pieceGm.transform.position = board[ToPos].transform.position;

        return capture || piece.Type == PieceType.Pawn;
    }
}

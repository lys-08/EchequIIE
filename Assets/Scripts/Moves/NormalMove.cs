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
            game.captureSound.Play();
            capture = true;
            GameObject.Destroy(board[ToPos].GetComponentInChildren<Piece>().gameObject);
        }
        else
        {
            game.moveSound.Play();
        }

        Piece piece = board[FromPos].GetComponentInChildren<Piece>();
        GameObject pieceGm = piece.gameObject;
        piece.HasMoved = true;
        pieceGm.transform.parent = board[ToPos].transform;
        pieceGm.transform.localScale = Vector3.one;
        pieceGm.transform.position = board[ToPos].transform.position;

        return capture || piece.Type == PieceType.Pawn;
    }
    
    /**
     * Execute itself on the copy of the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override bool ExecuteCopy(Piece[,] board)
    {
        bool capture = false;
        if (board[ToPos.Row, ToPos.Column] != null)
        {
            capture = true;
            board[ToPos.Row, ToPos.Column] = null;
        }

        board[ToPos.Row, ToPos.Column] = board[FromPos.Row, FromPos.Column];
        return capture || board[ToPos.Row, ToPos.Column].Type == PieceType.Pawn;
    }
}

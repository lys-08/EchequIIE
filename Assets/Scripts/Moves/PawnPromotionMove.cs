using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PawnPromotionMove : Move
{
    public override MoveType Type => MoveType.PawnPromotion;
    
    public override Position FromPos { get; }
    public override Position ToPos { get; }

    private PieceType newType;

    public PawnPromotionMove(Position fromPos, Position toPos, PieceType newType)
    {
        FromPos = fromPos;
        ToPos = toPos;
        this.newType = newType;
    }

    /**
     *
     */
    private GameObject CreatePromotionPiece(Player color, Transform parent)
    {
        Board board = GameObject.FindObjectOfType<Board>();
        List<GameObject> pieces;

        if (color == Player.Black) pieces = board.blackPieces;
        else pieces = board.whitePieces;
        
        GameObject obj = null;
        switch (newType)
        {
            case PieceType.Knight:
                obj = GameObject.Instantiate(pieces[1], parent);
                obj.transform.localScale = Vector3.one;
                obj.AddComponent<Knight>();
                obj.GetComponent<Knight>().Color = color;
                break;
            case PieceType.Bishop:
                obj = GameObject.Instantiate(pieces[2], parent);
                obj.transform.localScale = Vector3.one;
                obj.AddComponent<Bishop>();
                obj.GetComponent<Bishop>().Color = color;
                break;
            case PieceType.Queen:
                obj = GameObject.Instantiate(pieces[3], parent);
                obj.transform.localScale = Vector3.one;
                obj.AddComponent<Queen>();
                obj.GetComponent<Queen>().Color = color;
                break;
            case PieceType.Rook:
                obj = GameObject.Instantiate(pieces[0], parent);
                obj.transform.localScale = Vector3.one;
                obj.AddComponent<Rook>();
                obj.GetComponent<Rook>().Color = color;
                break;
        }

        return obj;
    }

    public override void Execute(Board board)
    {
        Piece pawn = board[FromPos].GetComponentInChildren<Piece>();
        GameObject.Destroy(board[FromPos].GetComponentInChildren<Piece>().GameObject());

        Piece promotionPiece = CreatePromotionPiece(pawn.Color, board[ToPos].transform).GetComponent<Piece>();
        promotionPiece.HasMoved = true;
        //promotionPiece.transform.position = 0.02f * new Vector3(1.25f * ToPos.Column, 0.05f, 1.25f * ToPos.Row);
    }
}

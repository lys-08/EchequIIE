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
     * Create the promotion piece chosen
     */
    private GameObject CreatePromotionPiece(Player color, Transform parent)
    {
        Board board = GameObject.FindObjectOfType<Board>();
        List<GameObject> pieces;

        pieces = color == Player.Black ? board.blackPieces : board.whitePieces;
        
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

    /**
     * Execute itself on the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override bool Execute(Board board)
    {
        Piece pawn = board[FromPos].GetComponentInChildren<Piece>();
        if (!board.IsEmpty(ToPos)) GameObject.Destroy(board[ToPos].GetComponentInChildren<Piece>().gameObject);

        Piece promotionPiece = CreatePromotionPiece(pawn.Color, board[ToPos].transform).GetComponent<Piece>();
        promotionPiece.HasMoved = true;
        
        return true; // always move a pawn
    }
}

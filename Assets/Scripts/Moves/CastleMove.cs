using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleMove : Move
{
    public override MoveType Type { get; }
    public override Position FromPos { get; }
    public override Position ToPos { get; }

    private Direction kingMoveDir;
    private Position rookFromPos;
    private Position rookToPos;

    public CastleMove(MoveType type, Position kingPos)
    {
        Type = type;
        FromPos = kingPos;

        // Castle on the king size
        if (type == MoveType.CastleKS)
        {
            kingMoveDir = Direction.East;
            ToPos = new Position(kingPos.Row, 6);
            rookFromPos = new Position(kingPos.Row, 7);
            rookToPos = new Position(kingPos.Row, 5);
        }
        // Castle on the queen size
        else if (type == MoveType.CastleQS)
        {
            kingMoveDir = Direction.West;
            ToPos = new Position(kingPos.Row, 2);
            rookFromPos = new Position(kingPos.Row, 0);
            rookToPos = new Position(kingPos.Row, 3);
        }
    }
    
    /**
     * Execute itself on the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override bool Execute(Board board)
    {
        new NormalMove(FromPos, ToPos).Execute(board);
        new NormalMove(rookFromPos, rookToPos).Execute(board);
        
        game.castleSound.Play();
        return false; // never capture a piece nor move a pawn
    }
    
    /**
     * Execute itself on the copy of the board. Returns true if a piece is captured or a pawn moved
     * -> like the command pattern
     */
    public override void ExecuteCopy(Piece[,] board)
    {
        new NormalMove(FromPos, ToPos).ExecuteCopy(board);
        new NormalMove(rookFromPos, rookToPos).ExecuteCopy(board);
    }
    
    /**
     * Return true if the move does not leave the current player's king in check
     *
     */
    public override bool IsLegal(Board board)
    {
        Player player = board[FromPos].GetComponentInChildren<Piece>().Color;
        
        /*
         * The rook cannot happened if the king is in direct check
         */
        if (board.IsInCheck(player)) return false;
        
        Piece[,] boardCopy = board.Copy();
        Position kingPosInCopy = FromPos;
        for (int i = 0; i < 2; i++)
        {
            new NormalMove(kingPosInCopy, kingPosInCopy + kingMoveDir).ExecuteCopy(boardCopy);
            kingPosInCopy += kingMoveDir;

            // The castle cannot happened if the king if in check after
            if (board.IsInCheckCopy(boardCopy, player)) return false;
        }
        
        return true;
    }
}

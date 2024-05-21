using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : Piece
{
    public override PieceType Type => PieceType.Pawn;
    public override Player Color { get; }

    private readonly Direction forward;

    
    public Pawn(Player color)
    {
        Color = color;
        
        if (color == Player.White) forward = Direction.North;
        else if (color == Player.Black) forward = Direction.South;
    }

    public override Piece Copy()
    {
        Pawn copy = new Pawn(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }

    /**
     * Return true id the pawn can move forward into the given position
     */
    private bool CanMoveTo(Position pos, Board board)
    {
        return Board.IsInside(pos) && board.IsEmpty(pos);
    }

    /**
     * Returns true if the pawn can move diagonally into the given position
     */
    private bool CanCaptureAt(Position pos, Board board)
    {
        /*
         * 1. If the position is outside the board or empty, than we can't capture something
         * 2. If the position is not empty, a piece can be captured only if it's an opponent piece
         */
        // 1
        if (!Board.IsInside(pos) || board.IsEmpty(pos)) return false;
        
        // 2
        return board[pos].Color != Color;
    }

    /**
     * Returns the possible moves that the pawn can make
     */
    private IEnumerable<Move> ForwardMoves(Position pos, Board board)
    {
        /*
         * 1. We determine if the pawn can move forward by 1 board case
         * 2. If it's possible, we check if the pawn has already moved. If he has, then we do nothing. Otherwise,
         *    we check if he can move forward again 
         */
        Position onMovePos = pos + forward;
        if (CanMoveTo(onMovePos, board)) // 1
        {
            yield return new NormalMove(pos, onMovePos);

            Position twoMovePos = onMovePos + forward;
            if (!HasMoved && CanMoveTo(onMovePos, board)) // 2
            {
                yield return new NormalMove(pos, twoMovePos);
            }
        }
    }
    
    /**
     * Returns the possible diagonal moves that the pawn can make (to capture a piece)
     */
    private IEnumerable<Move> DiagonalMoves(Position pos, Board board)
    {
        /*
         * For both west and east direction, we check if we can capture a piece
         */
        foreach (Direction dirs in new Direction[] { Direction.West, Direction.East })
        {
            Position toPos = pos + forward + dirs;
            if (CanCaptureAt(toPos, board)) yield return new NormalMove(pos, toPos);
        }
    }
    
    /**
     * Returns a collection containing all the moves the piece can make
     */
    public override IEnumerable<Move> GetMoves(Position pos, Board board)
    {
        return ForwardMoves(pos, board).Concat(DiagonalMoves(pos, board));
    }
}
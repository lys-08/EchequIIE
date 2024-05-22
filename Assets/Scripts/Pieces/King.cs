using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override PieceType Type => PieceType.King;
    public override Player Color { get; set; }
    
    // Directions of the king
    private static readonly Direction[] directions = new Direction[]
    {
        Direction.North,
        Direction.South,
        Direction.East,
        Direction.West,
        Direction.NorthEast,
        Direction.NorthWest,
        Direction.SouthEast,
        Direction.SouthWest
    };

    
    public King(Player color)
    {
        Color = color;
    }

    public override Piece Copy()
    {
        King copy = new King(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }
    
    /**
     * Returns the position where the king is allow to move
     */
    private IEnumerable<Position> MovePositions(Position pos, Board board)
    {
        foreach (Direction dir in directions)
        {
            Position toPos = pos + dir;
            if (Board.IsInside(toPos)) continue; // The position is inside the board
            if (board.IsEmpty(toPos) || board[toPos].GetComponentInChildren<Piece>().Color != Color) yield return toPos; // there is no piece or there is an opponent piece
        }
    }

    /**
     * Returns a collection containing all the moves the piece can make
     */
    public override IEnumerable<Move> GetMoves(Position pos, Board board)
    {
        foreach (Position toPos in MovePositions(pos, board))
        {
            yield return new NormalMove(pos, toPos);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bishop : Piece
{
    public override PieceType Type => PieceType.Bishop;
    public override Player Color { get; set; }

    // Directions of the bishop
    private static readonly Direction[] directions = new Direction[]
    {
        Direction.NorthEast,
        Direction.NorthWest,
        Direction.SouthEast,
        Direction.SouthWest
    };

    
    public Bishop(Player color)
    {
        Color = color;
    }

    public override Piece Copy()
    {
        Bishop copy = new Bishop(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }

    /**
     * Returns a collection containing all the moves the piece can make
     */
    public override IEnumerable<Move> GetMoves(Position pos, Board board)
    {
        return MovePositionsDir(pos, board, directions).Select(toPos => new NormalMove(pos, toPos));
    }
}

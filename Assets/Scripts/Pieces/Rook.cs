using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rook : Piece
{
    public override PieceType Type => PieceType.Pawn;
    public override Player Color { get; set; }
    
    // Directions of the rook
    private static readonly Direction[] directions = new Direction[]
    {
        Direction.North,
        Direction.South,
        Direction.East,
        Direction.West
    };

    public override Piece Copy()
    {
        Rook copy = new Rook();
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

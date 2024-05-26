using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Queen : Piece
{
    public override PieceType Type => PieceType.Queen;
    public override Player Color { get; set; }
    
    // Directions of the queen
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
    

    public Queen(Player color)
    {
        Color = color;
    }
    
    /**
     * Returns a collection containing all the moves the piece can make
     */
    public override IEnumerable<Move> GetMoves(Position pos, Board board)
    {
        return MovePositionsDir(pos, board, directions).Select(toPos => new NormalMove(pos, toPos));
    }
    
    /**
     * Returns a collection containing all the moves the piece can make
     */
    public override IEnumerable<Move> GetMovesCopy(Position pos, Piece[,] board)
    {
        return MovePositionsDirCopy(pos, board, directions).Select(toPos => new NormalMove(pos, toPos));
    }
}

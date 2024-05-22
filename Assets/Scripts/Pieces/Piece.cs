using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Piece : MonoBehaviour
{
    public abstract PieceType Type { get; }
    public abstract Player Color { get; set; }
    public bool HasMoved { get; set; } = false;

    
    public abstract Piece Copy();
    
    /**
     * Returns a collection containing all the moves the piece can make
     */
    public abstract IEnumerable<Move> GetMoves(Position pos, Board board);

    /**
     * Returns all reacheable positions in a given direction
     */
    public IEnumerable<Position> MovePositionsDir(Position fromPos, Board board, Direction direction)
    {
        /*
         * 1. For each position on the board in the direction given, we checked if the position is empty
         * 2. If the position is empty, we can place the piece. Otherwise, if the piece already here has another
         *    color then we take the piece
         */
        for (Position pos = fromPos + direction; Board.IsInside(pos); pos += direction)
        {
            if (board.IsEmpty(pos))
            {
                yield return pos;
                continue;
            }

            Piece piece = board[pos].GetComponent<Piece>();
            if (piece.Color != Color) yield return pos;
            
            yield break;
        }
    }
    
    /**
     * Returns all reacheable positions in all given direction
     */
    public IEnumerable<Position> MovePositionsDir(Position fromPos, Board board, Direction[] directions)
    {
        return directions.SelectMany(dir => MovePositionsDir(fromPos, board, dir));
    }
}

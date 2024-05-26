using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knight : Piece
{
    public override PieceType Type => PieceType.Knight;
    public override Player Color { get; set; }

    
    public Knight(Player color)
    {
        Color = color;
    }

    /**
     * Returns the position where the knight can potentially move
     */
    private static IEnumerable<Position> PotentialPositions(Position pos)
    {
        foreach (Direction verticalDir in new Direction[] {Direction.North, Direction.South })
        {
            foreach (Direction horizontalDir in new Direction[] {Direction.East, Direction.West })
            {
                yield return pos + 2 * verticalDir + horizontalDir;
                yield return pos + 2 * horizontalDir + verticalDir;
            }
        }
    }

    /**
     * Returns the position where the knight is allow to move
     * -> A position where the knight is allow to move is a position :
     *      - inside the board
     *      - where there is no chess piece or the chess piece is one of the opponent's
     */
    private IEnumerable<Position> MovePositions(Position pos, Board board)
    {
        return PotentialPositions(pos).Where(pos => Board.IsInside(pos)
                                                    && (board.IsEmpty(pos) || board[pos].GetComponentInChildren<Piece>().Color != Color));
    }

    /**
     * Returns a collection containing all the moves the piece can make
     */
    public override IEnumerable<Move> GetMoves(Position pos, Board board)
    {
        return MovePositions(pos, board).Select(toPos => new NormalMove(pos, toPos));
    }
}

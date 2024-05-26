using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /**
     * Returns true if the rook is unmoved
     */
    private bool IsUnmovedRook(Position pos, Board board)
    {
        // If there is no pieces than the rook has moved
        if (board.IsEmpty(pos)) return false;

        // Otherwise we check if the piece is the rook and if it have move or not
        Piece piece = board[pos].GetComponentInChildren<Piece>();
        return piece.Type == PieceType.Rook && !piece.HasMoved;
    }

    /**
     * Returns true if all the positions between the king and the rook are empty
     */
    private bool AllEmptyForCastle(IEnumerable<Position> positions, Board board)
    {
        return positions.All(pos => board.IsEmpty(pos));
    }

    /**
     * Return true if all condition to do a Castle on the King Side are valid
     */
    private bool CanCastleKS(Position fromPos, Board board)
    {
        // If the king has moved then the castle cannot happened
        if (HasMoved) return false;

        Position rookPos = new Position(fromPos.Row, 7);
        Position[] betweenPos = new Position[] { new(fromPos.Row, 5), new(fromPos.Row, 6) };

        return IsUnmovedRook(rookPos, board) && AllEmptyForCastle(betweenPos, board);
    }
    
    /**
     * Return true if all condition to do a Castle on the Queen Side are valid
     */
    private bool CanCastleQS(Position fromPos, Board board)
    {
        // If the king has moved then the castle cannot happened
        if (HasMoved) return false;

        Position rookPos = new Position(fromPos.Row, 0);
        Position[] betweenPos = new Position[] { new(fromPos.Row, 1), new(fromPos.Row, 2), new(fromPos.Row, 3) };

        return IsUnmovedRook(rookPos, board) && AllEmptyForCastle(betweenPos, board);
    }
    
    /**
     * Returns the position where the king is allow to move
     */
    private IEnumerable<Position> MovePositions(Position pos, Board board)
    {
        foreach (Direction dir in directions)
        {
            Position toPos = pos + dir;
            if (!Board.IsInside(toPos)) continue; // The position is inside the board
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

        if (CanCastleKS(pos, board))
        {
            yield return new CastleMove(MoveType.CastleKS, pos);
        }
        if (CanCastleQS(pos, board)) yield return new CastleMove(MoveType.CastleQS, pos);
    }
    
    /**
     * Returns true if the opponent's king is in check
     */
    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return MovePositions(from, board).Any(toPos =>
        {
            Piece piece = board[toPos].GetComponentInChildren<Piece>();
            return piece != null && piece.Type == PieceType.King;
        });
    }
    
    /**
     * Returns true if the opponent's king is in check
     */
    public virtual bool CanCaptureOpponentKingCopy(Position from, Board board, Piece[,] pieces)
    {
        return MovePositions(from, board).Any(toPos =>
        {
            Piece piece = pieces[toPos.Row, toPos.Column];
            return piece != null && piece.Type == PieceType.King;
        });
    }
}

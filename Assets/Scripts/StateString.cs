using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StateString
{
    private StringBuilder sb = new StringBuilder();

    public StateString(Player currentPlayer, Board board)
    {
        AddPiecePlacement(board);
        sb.Append(' ');
        AddCurrentPlayer(currentPlayer);
        sb.Append(' ');
        AddCastlingRights(board);
        sb.Append(' ');
        AddEnPassant(board, currentPlayer);
    }

    /**
     * Returns a representative character for the parameter piece
     */
    private static char PieceChar(Piece piece)
    {
        char c = piece.Type switch
        {
            PieceType.Pawn => 'p',
            PieceType.Knight => 'n',
            PieceType.Bishop => 'b',
            PieceType.Rook => 'r',
            PieceType.Queen => 'q',
            PieceType.King => 'k',
            _ => ' '
        };

        /*
         * Lower => black pieces
         * Upper => white pieces
         */
        if (piece.Color == Player.White)
        {
            return char.ToUpper(c);
        }

        return c;
    }

    /**
     * Each piece will be encoded using it's representative character and empty square will be encoded as integer
     * Takes the board and the row to add
     */
    private void AddRowData(Board board, int row)
    {
        int empty = 0; // counter for the consecutive empty squares

        for (int c = 0; c < 8; c++) // Loop on the columns
        {
            // The position is empty => incrementation of empty and continue to the next position
            if (board[row, c].GetComponentInChildren<Piece>() == null)
            {
                empty++;
                continue;
            }

            // We had encounter the piece
            if (empty > 0)
            {
                sb.Append(empty);
                empty = 0;
            }
            sb.Append(PieceChar(board[row, c].GetComponentInChildren<Piece>()));
        }

        if (empty > 0) sb.Append(empty);
    }

    /**
     * Add the entire piece placement of the board to the state string
     */
    private void AddPiecePlacement(Board board)
    {
        for (int r = 0; r < 8; r++)
        {
            if (r != 0) sb.Append('/'); // each row will be separated by a /
            AddRowData(board, r);
        }
    }

    /**
     * Add the current player to the state string
     */
    private void AddCurrentPlayer(Player currentPlayer)
    {
        if (currentPlayer == Player.White) sb.Append('w');
        else sb.Append('b');
    }

    /**
     *Add the castling rights (king and queen side for both players) to the state string
     */
    private void AddCastlingRights(Board board)
    {
        bool castleWKS = board.CastleRightKS(Player.White);
        bool castleWQS = board.CastleRightQS(Player.White);
        bool castleBKS = board.CastleRightKS(Player.Black);
        bool castleBQS = board.CastleRightQS(Player.Black);

        if (!(castleWKS || castleWQS || castleBKS || castleBQS))
        {
            sb.Append('-');
            return;
        }

        if (castleWKS) sb.Append('K');
        if (castleWQS) sb.Append('Q');
        if (castleBKS) sb.Append('k');
        if (castleBQS) sb.Append('q');
    }

    /**
     *
     */
    private void AddEnPassant(Board board, Player currentPlayer)
    {
        // The current player can't take on passant
        if (!board.CanCaptureEnPassant(currentPlayer))
        {
            sb.Append('-');
            return;
        }

        Player opponent = currentPlayer == Player.White ? Player.Black : Player.White;
        Position pos = board.GetPawnSkipPosition(opponent); // En passant target square
        char file = (char)('a' + pos.Column);
        int rank = 8 - pos.Row;
        sb.Append(file);
        sb.Append(rank);
    }
}
    
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StateString
{
    public string CreateStateString(Player currentPlayer, Board board)
    {
        string s = "";
        s += AddPiecePlacement(board);
        s += ' ';
        s += AddCurrentPlayer(currentPlayer);
        s += ' ';
        s += AddCastlingRights(board);
        s += ' ';
        s += AddEnPassant(board, currentPlayer);

        return s;
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
    private string AddRowData(Board board, int row)
    {
        string s = "";
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
                s += empty;
                empty = 0;
            }
            s += PieceChar(board[row, c].GetComponentInChildren<Piece>());
        }

        if (empty > 0) s += empty;

        return s;
    }

    /**
     * Add the entire piece placement of the board to the state string
     */
    private string AddPiecePlacement(Board board)
    {
        string s = "";
        for (int r = 0; r < 8; r++)
        {
            if (r != 0) s += '/'; // each row will be separated by a /
            s += AddRowData(board, r);
        }
        
        return s;
    }

    /**
     * Add the current player to the state string
     */
    private string AddCurrentPlayer(Player currentPlayer)
    {
        string s = "";
        if (currentPlayer == Player.White) s += 'w';
        else s += 'b';

        return s;
    }

    /**
     *Add the castling rights (king and queen side for both players) to the state string
     */
    private string AddCastlingRights(Board board)
    {
        string s = "";
        bool castleWKS = board.CastleRightKS(Player.White);
        bool castleWQS = board.CastleRightQS(Player.White);
        bool castleBKS = board.CastleRightKS(Player.Black);
        bool castleBQS = board.CastleRightQS(Player.Black);

        if (!(castleWKS || castleWQS || castleBKS || castleBQS))
        {
            s += '-';
            return s;
        }

        if (castleWKS) s += 'K';
        if (castleWQS) s += 'Q';
        if (castleBKS) s += 'k';
        if (castleBQS) s += 'q';

        return s;
    }

    /**
     *
     */
    private string AddEnPassant(Board board, Player currentPlayer)
    {
        string s = "";
        // The current player can't take on passant
        if (!board.CanCaptureEnPassant(currentPlayer))
        {
            s += '-';
            return s;
        }

        Player opponent = currentPlayer == Player.White ? Player.Black : Player.White;
        Position pos = board.GetPawnSkipPosition(opponent); // En passant target square
        char file = (char)('a' + pos.Column);
        int rank = 8 - pos.Row;
        s += file;
        s += rank;

        return s;
    }
}
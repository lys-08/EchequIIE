using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    // The board is an array of pieces
    private readonly Piece[,] pieces = new Piece[8, 8];
    
    
    /**
     * Return the piece at the row and column given
     */
    public Piece this[int row, int column]
    {
        get { return pieces[row, column]; }
        set { pieces[row, column] = value; }
    }

    /**
     * Return the piece at the position given
     */
    public Piece this[Position pos]
    {
        get { return pieces[pos.Row, pos.Column]; }
        set { pieces[pos.Row, pos.Column] = value; }
    }
    
    /**
     * Return a board initialized
     */
    public static Board InitBoard()
    {
        Board board = new Board();
        board.AddStartingPieces();
        return board;
    }

    /**
     * Add the pieces on the board
     * -> This function must be called at the initialization of the board
     */
    private void AddStartingPieces()
    {
        this[0, 0] = new Rook(Player.Black);
        this[0, 1] = new Knight(Player.Black);
        this[0, 2] = new Bishop(Player.Black);
        this[0, 3] = new Queen(Player.Black);
        this[0, 4] = new King(Player.Black);
        this[0, 5] = new Bishop(Player.Black);
        this[0, 6] = new Knight(Player.Black);
        this[0, 7] = new Rook(Player.Black);
        
        this[7, 0] = new Rook(Player.White);
        this[7, 1] = new Knight(Player.White);
        this[7, 2] = new Bishop(Player.White);
        this[7, 3] = new Queen(Player.White);
        this[7, 4] = new King(Player.White);
        this[7, 5] = new Bishop(Player.White);
        this[7, 6] = new Knight(Player.White);
        this[7, 7] = new Rook(Player.White);

        for (int i = 0; i < 8; i++)
        {
            this[1, i] = new Pawn(Player.Black);
            this[6, i] = new Pawn(Player.White);
        }
    }

    /**
     * Return true if the position is inside the board. Otherwise, return false
     */
    public static bool IsInside(Position pos)
    {
        return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
    }

    /**
     * Return true is there is no piece on this position of the board. Otherwise, return false
     */
    public bool IsEmpty(Position pos)
    {
        return this[pos] == null;
    }
}

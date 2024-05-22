using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Player 
{
    None,
    White, 
    Black
}

public class Game : MonoBehaviour
{
    public Board Board { get; set; }
    public Image[,] Highlights = new Image[8, 8];
    private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
    private Position selectedPos = null;
    public Player CurrentPlayer { get; set; }
       
    // STATE
    private StateMachine stateMachine_;
    public StateMachine GamestateMachine => stateMachine_;
    

    /**
     * Returns all the moves that the piece at the given position can make.
     * If there is no pieces then return Empty
     */
    public IEnumerable<Move> LegalMovesForPiece(Position pos)
    {
        // We check if there is a piece of the player color at the given position
        if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
        {
            return Enumerable.Empty<Move>();
        }

        Piece piece = Board[pos];
        return piece.GetMoves(pos, Board);
    }

    /**
     * Execute the move picked by the player
     */
    public void MakeMove(Move move)
    {
        move.Execute(Board);
    }

    
    #region Unity Events

    private void Awake()
    {
        stateMachine_ = new StateMachine(this); // State

        // Initialisation of the board
        Board = Board.InitBoard();
    }

    private void Start()
    {
        stateMachine_.Initialize(stateMachine_.whiteState); // State
    }
    
    private void Update()
    {
        stateMachine_.Update();
    }
    
    #endregion
}
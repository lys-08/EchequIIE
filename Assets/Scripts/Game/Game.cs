using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Player 
{
    White, 
    Black
}

public class Game : MonoBehaviour
{ 
    [field: SerializeField] public Board Board { get; set; }
    public Player CurrentPlayer { get; set; }
    
    // STATE
    private StateMachine stateMachine_;
    public StateMachine GamestateMachine => stateMachine_;

    private Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
    public Position selectedPos = null;
    

    /**
     * Returns all the legal moves that the piece at the given position can make
     * If there is no pieces then return Empty
     */
    public IEnumerable<Move> LegalMovesForPiece(Position pos)
    {
        // We check if there is a piece of the player color at the given position
        if (Board.IsEmpty(pos) || Board[pos].GetComponentInChildren<Piece>().Color != CurrentPlayer)
        {
            return Enumerable.Empty<Move>();
        }

        Piece piece = Board[pos].GetComponentInChildren<Piece>();
        return piece.GetMoves(pos, Board);
        //IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
        //return moveCandidates.Where(move => move.IsLegal(Board)); // Only the legal move are returned
    }

    /**
     * Execute the move picked by the player
     */
    public void MakeMove(Move move)
    {
        move.Execute(Board);
        if (CurrentPlayer == Player.Black)
        {
            stateMachine_.TransitionTo(stateMachine_.whiteState);
        }
        else
        {
            stateMachine_.TransitionTo(stateMachine_.blackState);
        }
    }

    /**
     * Takes the collection of legal moves for the selected piece and stores them
     */
    public void CacheMoves(IEnumerable<Move> moves)
    {
        // We clear the cache
        moveCache.Clear();
        HideHighlights();

        /*
         * For each moves, we store them in the dictionary
         * -> the key is toPos
         */
        foreach (Move move in moves)
        {
            Debug.Log("alors ???");
            moveCache[move.ToPos] = move;
        }
    }

    /**
     * Show the highlights of the possible moves in the cache
     */
    public void ShowHighlights()
    {
        foreach (Position toPos in moveCache.Keys)
        {
            Board[toPos].GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
    
    /**
     * Hide the highlights of the possible moves in the cache
     */
    public void HideHighlights()
    {
        foreach (Position toPos in moveCache.Keys)
        {
            Board[toPos].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    /**
     * Methods called when a case is clicked and there is a selected piece
     * -> pos : position of the case clicked
     */
    public void OnFromPositionSelected(Position pos)
    {
        // We get all moves possible for a piece at the given position (if the piece has the good color)
        List<Move> moves = LegalMovesForPiece(pos).ToList();
        
        // If there is no
        if (moves.Any()) selectedPos = pos;
        CacheMoves(moves);
        ShowHighlights();
    }

    /**
     * Called to move a piece : a piece is already selected and the newt click will call the move
     */
    public void OnToPositionSelected(Position pos)
    {
        selectedPos = null;
        HideHighlights();

        if (moveCache.TryGetValue(pos, out Move move))
        {
            MakeMove(move);
            moveCache.Clear();
        }
    }
    
    #region Unity Events

    private void Awake()
    {
        stateMachine_ = new StateMachine(this); // State

        // Initialisation of the board
        Board = Board.GetComponentInChildren<Board>();
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
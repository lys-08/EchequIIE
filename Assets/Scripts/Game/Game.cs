using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Player 
{
    None, // For draw
    White, 
    Black
}

public class Game : MonoBehaviour
{ 
    [field: SerializeField] public Board Board { get; set; }
    [SerializeField] public GameObject promotionMenu;
    [SerializeField] public GameObject gameOverMenu;
    
    [SerializeField] private AudioSource moveSound;
    
    public Player CurrentPlayer { get; set; }
    

    private Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
    public Position selectedPos = null;

    public Result Result { get; private set; } = null;
    
    private int noCaptureOrPawnMovesCount = 0; // count the consecutive none capturing and none pawn moves
    
    // STATE
    private StateMachine stateMachine_;
    public StateMachine GamestateMachine => stateMachine_;
    

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
        return piece.GetMoves(pos, Board);// TODO OO
        IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
        return moveCandidates.Where(move => move.IsLegal(Board)); // TODO Only the legal move are returned
    }

    /**
     * Execute the move picked by the player
     */
    public void MakeMove(Move move)
    {
        moveSound.Play();
        Board.SetPawnSkipPosition(CurrentPlayer, null);
        bool captureOrPawn = move.Execute(Board);

        if (captureOrPawn) noCaptureOrPawnMovesCount = 0;
        else noCaptureOrPawnMovesCount++;
        
        CheckForGameOver();
        
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
            if (move.Type == MoveType.PawnPromotion)
            {
                StartCoroutine(HandlePromotion(move.FromPos, move.ToPos));
            }
            else
            {
                MakeMove(move);
            }
            moveCache.Clear();
        }
    }

    /**
     * Handle the pawn promotion
     */
    public IEnumerator HandlePromotion(Position fromPos, Position toPos)
    {
        promotionMenu.SetActive(true);
        PromotionMenu promotionScript = promotionMenu.GetComponent<PromotionMenu>();
        
        while (!promotionScript.hasChoose)
        {
            yield return null;
        }
        
        Move promMove = new PawnPromotionMove(fromPos, toPos, promotionScript.newPiece);
        promotionScript.hasChoose = false;
        promotionMenu.SetActive(false);
        MakeMove(promMove);
    }

    /**
     * Returns all moves the player can make
     */
    public IEnumerable<Move> AllLegalMovesFor(Player player)
    {
        IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
        {
            Piece piece = Board[pos].GetComponentInChildren<Piece>();
            return piece.GetMoves(pos, Board);
        });

        return moveCandidates;//.Where(move => move.IsLegal(Board));// TODO : check detection
    }

    /**
     * Check if there is game over
     */
    private void CheckForGameOver()
    {
        if (!AllLegalMovesFor(CurrentPlayer).Any())
        {
            if (Board.IsInCheck(CurrentPlayer))
            {
                if (CurrentPlayer == Player.Black) Result = Result.Win(Player.White);
                else Result = Result.Win(Player.Black);
                
                stateMachine_.TransitionTo(stateMachine_.gameOverState);
            }
            else
            {
                Result = Result.Draw(EndReason.Stalemate);
                CurrentPlayer = Player.None;
                stateMachine_.TransitionTo(stateMachine_.gameOverState);
            }
        }
        else if (Board.InsufficientMaterial())
        {
            Result = Result.Draw(EndReason.InsufficientMaterial);
            stateMachine_.TransitionTo(stateMachine_.gameOverState);
        }
        else if (FiftyMoveRule())
        {
            Result = Result.Draw(EndReason.FiftyMoveRule);
            stateMachine_.TransitionTo(stateMachine_.gameOverState);
        }
    }

    public bool IsGameOver()
    {
        return Result != null;
    }

    /**
     * Returns true if the fifty move rule has been reach : if any capture nor pawn move are
     * made in 50 full moves than it's a draw
     */
    private bool FiftyMoveRule()
    {
        int fullMoves = noCaptureOrPawnMovesCount / 2; // One full move is made when each player have made a move

        return fullMoves == 50;
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
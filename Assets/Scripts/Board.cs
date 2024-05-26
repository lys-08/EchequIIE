using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Prefabs
    [SerializeField] public List<GameObject> whitePieces = new List<GameObject>();
    [SerializeField] public List<GameObject> blackPieces = new List<GameObject>();
    [SerializeField] private GameObject piece;
    
    // The board is an array of pieces
    private GameObject[,] pieces = new GameObject[8,8];
    // We store the position of the pawn that is moved 2 cases forward
    private Dictionary<Player, Position> pawnSkipPositions = new Dictionary<Player, Position>
    {
        { Player.White, null },
        { Player.Black, null },
    };

    private Counter counter;


    private void Awake()
    {
        counter = FindObjectOfType<Counter>();
    }

    /**
     * Return the piece at the row and column given
     */
    public GameObject this[int row, int column]
    {
        get { return pieces[row, column]; }
        set { pieces[row, column] = value; }
    }

    /**
     * Return the piece at the position given
     */
    public GameObject this[Position pos]
    {
        get {return pieces[pos.Row, pos.Column]; }
        set { pieces[pos.Row, pos.Column] = value; }
    }
    
    public Position GetPawnSkipPosition(Player player)
    {
        return pawnSkipPositions[player];
    }
    
    public void SetPawnSkipPosition(Player player, Position pos)
    {
        pawnSkipPositions[player] = pos;
    }
    
    /**
     * Return a board initialized
     */
    public Board InitBoard()
    {
        // Instantiation of blank cases
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var piece1 = Instantiate<GameObject>(this.piece, this.gameObject.transform);
                piece1.transform.localScale = Vector3.one;
                piece1.name = "Row : " + i + ", Col : " + j;
                piece1.transform.localPosition = new Vector3(j * 1.25f, 0.01f,  i * 1.25f);
                piece1.GetComponent<ExampleTouch>().SetPosition(i, j);
                pieces[i, j] = piece1;
            }
        }
        
        // Adding the pieces
        AddStartingPieces();
        
        // Return the board initialized
        return this;
    }

    /**
     * Add the pieces on the board
     * -> This function must be called at the initialization of the board
     */
    private void AddStartingPieces()
    {
        // BLACK PIECES
        var obj = Instantiate(blackPieces[0], this[7,0].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[0], this[7,7].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[1], this[7,1].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[1], this[7,6].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[2], this[7,2].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[2], this[7,5].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[3], this[7,3].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        obj.AddComponent<Queen>();
        obj.GetComponent<Queen>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[4], this[7,4].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<King>();
        obj.GetComponent<King>().Color = Player.Black;
        
        
        // White PIECES
        obj = Instantiate(whitePieces[0], this[0,0].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.White;
        
        obj = Instantiate(whitePieces[0], this[0,7].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.White;
        
        obj = Instantiate(whitePieces[1], this[0,1].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.White;
        
        obj = Instantiate(whitePieces[1], this[0,6].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.White;
        
        obj = Instantiate(whitePieces[2], this[0,2].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.White;
        
        obj = Instantiate(whitePieces[2], this[0,5].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.White;
        
        obj = Instantiate(whitePieces[3], this[0,3].transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        obj.AddComponent<Queen>();
        obj.GetComponent<Queen>().Color = Player.White;
        
        obj = Instantiate(whitePieces[4], this[0,4].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<King>();
        obj.GetComponent<King>().Color = Player.White;
        

        for (int i = 0; i < 8; i++)
        {
            obj = Instantiate(blackPieces[5], this[6,i].transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.rotation = Quaternion.Euler(0, 90, 0);
            obj.AddComponent<Pawn>();
            obj.GetComponent<Pawn>().Color = Player.Black;
            
            obj = Instantiate(whitePieces[5], this[1,i].transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.rotation = Quaternion.Euler(0, -90, 0);
            obj.AddComponent<Pawn>();
            obj.GetComponent<Pawn>().Color = Player.White;
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
        Piece p = this[pos].GetComponentInChildren<Piece>();
        return p == null;
    }

    /**
     * Returns all the pieces position on the board
     */
    public IEnumerable<Position> PiecePositions()
    {
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Position pos = new Position(r, c);

                if (!IsEmpty(pos)) yield return pos;
            }
        }
    }

    /**
     * Returns all the pieces positions of a player
     */
    public IEnumerable<Position> PiecePositionsFor(Player player)
    {
        return PiecePositions().Where(pos => this[pos].GetComponentInChildren<Piece>().Color == player);
    }

    /**
     * Returns true if the player's king is in check
     */
    public bool IsInCheck(Player player)
    {
        Player color = player == Player.Black ? Player.White : Player.Black;

        return PiecePositionsFor(color).Any(pos =>
        {
            Piece piece = this[pos].GetComponentInChildren<Piece>();
            return piece.CanCaptureOpponentKing(pos, this);
        });
    }
    
    /**
     * Returns true if the player's king is in check
     */
    public bool IsInCheckCopy(Piece[,] board, Player player)
    {
        Player color = player == Player.Black ? Player.White : Player.Black;

        // PiecePositionFor
        Dictionary<Position, Piece> pieceList = new Dictionary<Position, Piece>();
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                if (board[r, c] != null && board[r, c].Color == color)
                {
                    pieceList.Add(new Position(r, c), board[r, c]);
                }
            }
        }
        
        return pieceList.Any(piece1 =>
        {
            // Can capture opponent king
            return piece1.Value.GetMoves(piece1.Key, this).Any(move =>
            {
                Piece p = board[move.ToPos.Row, move.ToPos.Column];
                return p != null && p.Type == PieceType.King;
            });
        });
    }

    /**
     * Copy the board
     */
    public Piece[,] Copy()
    {
        Piece[,] copy = new Piece[8,8];

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                copy[r, c] = this[r, c].GetComponentInChildren<Piece>();
            }
        }
        return copy;
    }

    /**
     * Returns a counting of all pieces on the board
     */
    public Counter CountPieces()
    {
        foreach (Position pos in PiecePositions())
        {
            Piece piece = this[pos].GetComponentInChildren<Piece>();
            counter.Increment(piece.Color, piece.Type);
        }

        return counter;
    }

    /**
     * Returns true if the remaining pieces on the board are insufficient for either player
     * for ever checkmate the other
     */
    public bool InsufficientMaterial()
    {
        return IsKingVsKing(counter) || IsKingBishopVsKing(counter) ||
               IsKingKnightVsKing(counter) || IsKingKBishopVs(counter);
    }

    /**
     * Returns true if there is only 2 pieces left (they are the king of each player because otherwise it would be checkmate)
     */
    private static bool IsKingVsKing(Counter counter)
    {
        return counter.TotalCount == 2;
    }

    /**
     * Returns true if there is a king and a bishop versus a king only
     */
    private static bool IsKingBishopVsKing(Counter counter)
    {
        return counter.TotalCount == 3 &&
               (counter.White(PieceType.Bishop) == 1 || counter.Black(PieceType.Bishop) == 1);
    }
    
    /**
     * Returns true if there is a king and a knight versus a king only
     */
    private static bool IsKingKnightVsKing(Counter counter)
    {
        return counter.TotalCount == 3 &&
               (counter.White(PieceType.Knight) == 1 || counter.Black(PieceType.Knight) == 1);
    }
    
    /**
     * Returns true if there is a king and a bishop versus and both bishop are on the same color square
     */
    private bool IsKingKBishopVs(Counter counter)
    {
        if (counter.TotalCount != 4) return false;
        if (counter.White(PieceType.Bishop) != 1 || counter.Black(PieceType.Bishop) != 1) return false;

        Position whiteBishop = FindPiece(Player.White, PieceType.Bishop);
        Position blackBishop = FindPiece(Player.Black, PieceType.Bishop);

        return whiteBishop.SquareColor() == blackBishop.SquareColor();
    }

    /**
     * Returns the position of the first instance of a piece with the given color
     */
    private Position FindPiece(Player color, PieceType type)
    {
        return PiecePositionsFor(color).First(pos => this[pos].GetComponentInChildren<Piece>().Type == type);
    }

    /**
     * Returns true is the positions contained an unmoved king and unmoved rook
     */
    private bool IsUnmovedKingAndRook(Position kingPos, Position rookPos)
    {
        if (IsEmpty(kingPos) || IsEmpty(rookPos)) return false;

        Piece king = this[kingPos].GetComponentInChildren<Piece>();
        Piece rook = this[rookPos].GetComponentInChildren<Piece>();

        return king.Type == PieceType.King && rook.Type == PieceType.Rook &&
               !king.HasMoved && !rook.HasMoved;
    }

    /**
     * Returns true if the given player has the right to castle king side
     */
    public bool CastleRightKS(Player player)
    {
        return player switch
        {
            Player.White => IsUnmovedKingAndRook(new Position(0, 4), new Position(0, 7)),
            Player.Black => IsUnmovedKingAndRook(new Position(7, 4), new Position(7, 7)),
            _ => false
        };
    }
    
    /**
     * Returns true if the given player has the right to castle queen side
     */
    public bool CastleRightQS(Player player)
    {
        return player switch
        {
            Player.White => IsUnmovedKingAndRook(new Position(0, 4), new Position(0, 0)),
            Player.Black => IsUnmovedKingAndRook(new Position(7, 4), new Position(7, 0)),
            _ => false
        };
    }

    /**
     * Returns true if the given player has a pawn that can move to skipPos and capture en passant
     */
    private bool HasPawnInPosition(Player player, Position[] pawnPositions, Position skipPos)
    {
        foreach (Position pos in pawnPositions.Where(IsInside))
        {
            Piece piece = this[pos].GetComponentInChildren<Piece>();
            if (piece == null || piece.Color != player || piece.Type != PieceType.Pawn) continue;

            EnPassantMove move = new EnPassantMove(pos, skipPos);
            if (move.IsLegal(this)) return true;
        }
        
        return false;
    }

    /**
     * Returns true if the player can capture en passant in the current state
     */
    public bool CanCaptureEnPassant(Player player)
    {
        Player opponent = player == Player.White ? Player.Black : Player.White;
        Position skipPos = GetPawnSkipPosition(opponent);

        if (skipPos == null) return false;

        Position[] pawnPositions = player switch
        {
            Player.White => new Position[] { skipPos + Direction.NorthWest, skipPos + Direction.NorthEast },
            Player.Black => new Position[] { skipPos + Direction.SouthWest, skipPos + Direction.SouthEast },
            _ => Array.Empty<Position>()
        };

        return HasPawnInPosition(player, pawnPositions, skipPos);
    }
}


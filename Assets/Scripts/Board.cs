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
        get
        {
            //Debug.Log($"row :{pos.Row}, col :{pos.Column}");
            return pieces[pos.Row, pos.Column];
        }
        set { pieces[pos.Row, pos.Column] = value; }
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
                piece1.name = "Row : " + i + ", Col : " + j; // TODO : remove
                piece1.transform.position = new Vector3(j * 1.25f * 0.02f, 0.05f * 0.02f, i * 1.25f * 0.02f);
                piece1.GetComponent<ExampleTouch>().SetPosition(i, j);
                pieces[i, j] = piece1;
                Debug.Log($"INSTANTIATION DU CARRE : row :{i}, col :{j}");
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
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[0], this[7,7].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[1], this[7,1].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[1], this[7,6].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[2], this[7,2].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[2], this[7,5].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[3], this[7,3].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Queen>();
        obj.GetComponent<Queen>().Color = Player.Black;
        
        obj = Instantiate(blackPieces[4], this[7,4].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<King>();
        obj.GetComponent<King>().Color = Player.Black;
        
        
        // White PIECES
        obj = Instantiate(whitePieces[0], this[0,0].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.White;
        
        obj = Instantiate(whitePieces[0], this[0,7].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Rook>();
        obj.GetComponent<Rook>().Color = Player.White;
        
        obj = Instantiate(whitePieces[1], this[0,1].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.White;
        
        obj = Instantiate(whitePieces[1], this[0,6].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Knight>();
        obj.GetComponent<Knight>().Color = Player.White;
        
        obj = Instantiate(whitePieces[2], this[0,2].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.White;
        
        obj = Instantiate(whitePieces[2], this[0,5].transform);
        obj.transform.localScale = Vector3.one;
        obj.AddComponent<Bishop>();
        obj.GetComponent<Bishop>().Color = Player.White;
        
        obj = Instantiate(whitePieces[3], this[0,3].transform);
        obj.transform.localScale = Vector3.one;
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
            obj.AddComponent<Pawn>();
            obj.GetComponent<Pawn>().Color = Player.Black;
            
            obj = Instantiate(whitePieces[5], this[1,i].transform);
            obj.transform.localScale = Vector3.one;
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
        Piece piece = this[pos].GetComponentInChildren<Piece>();
        return piece == null;
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
        Player color;
        color = player == Player.Black ? Player.White : Player.Black;

        return PiecePositionsFor(color).Any(pos =>
        {
            Piece piece = this[pos].GetComponentInChildren<Piece>();
            return piece.CanCaptureOpponentKing(pos, this);
        });
    }

    /**
     * Copy the board
     */
    public Board Copy()
    {
        Board copy = Instantiate(this);
        return copy;
    }
}

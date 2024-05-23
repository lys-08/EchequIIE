using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotionMenu : MonoBehaviour
{
    public PieceType newPiece;
    public bool hasChoose = false;
    
    public void Queen()
    {
        newPiece = PieceType.Queen;
        hasChoose = true;
    }
    
    public void Bishop()
    {
        newPiece = PieceType.Bishop;
        hasChoose = true;
    }
    
    public void Rook()
    {
        newPiece = PieceType.Rook;
        hasChoose = true;
    }
    
    public void Knight()
    {
        newPiece = PieceType.Knight;
        hasChoose = true;
    }
}

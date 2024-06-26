using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType 
{
    Normal,
    CastleKS, // Rock on the king side
    CastleQS, // Rock on the queen side
    DoublePawn, // When the pawn first move, the player can move it by 2 cases
    EnPassant, // The take "en passant"
    PawnPromotion // When a pawn reach the other side of the board
}

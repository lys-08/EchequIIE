using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kight : Piece
{
    public override PieceType Type => PieceType.Pawn;
    public override Player Color { get; }

    
    public Kight(Player color)
    {
        Color = color;
    }

    public override Piece Copy()
    {
        Kight copy = new Kight(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }
}

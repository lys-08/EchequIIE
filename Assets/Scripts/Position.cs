using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Position
{
    public int Row { get; } // row index
    public int Column { get; } // column index

    /**
     * Constructor
     */
    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    /**
     * Return the color of the square
     */
    public Player SquareColor()
    {
        if ((Row + Column) % 2 == 0) return Player.White;

        return Player.Black;
    }

    public static bool operator==(Position pos1, Position pos2)
    {
        return EqualityComparer<Position>.Default.Equals(pos1, pos2);
    }
    
    public static bool operator!=(Position pos1, Position pos2)
    {
        return !(pos1 == pos2);
    }
    
    public static Position operator+(Position pos, Direction dir)
    {
        return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
    }
}

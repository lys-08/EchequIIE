using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Direction //: MonoBehaviour
{
    /**
     * Predefines Directions
     */
    public static Direction North = new Direction(-1, 0); // The (0,0) is the square at the top left corner
    public static Direction South = new Direction(1, 0);
    public static Direction East = new Direction(0, 1);
    public static Direction West = new Direction(0, -1);
    public static Direction NorthEast = North + East;
    public static Direction NorthWest = North + West;
    public static Direction SouthEast = South + East;
    public static Direction SouthWest = South + West;
    
    
    
    public int RowDelta { get; set; } // The row movement
    public int ColumnDelta { get; set; } // The column movement

    
    /**
     * Constructor
     */
    public Direction(int rowDelta, int columnDelta)
    {
        RowDelta = rowDelta;
        ColumnDelta = columnDelta;
    }

    public static Direction operator+(Direction dir1, Direction dir2)
    {
        return new Direction(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
    }
    
    public static Direction operator*(Direction dir, int i)
    {
        return new Direction(dir.RowDelta * i, dir.ColumnDelta * i);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private Dictionary<PieceType, int> whiteCounter = new Dictionary<PieceType, int>();
    private Dictionary<PieceType, int> blackCounter = new Dictionary<PieceType, int>();
    
    // Number of pieces on the board
    public int TotalCount { get; private set; }


    private void Awake()
    {
        foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
        {
            whiteCounter[type] = 0;
            blackCounter[type] = 0;
        }
    }

    /**
     * Increment the number of the corresponding piece of a player
     */
    public void Increment(Player color, PieceType type)
    {
        if (color == Player.White)  whiteCounter[type]++;
        else blackCounter[type]++;

        TotalCount++;
    }

    /**
     * Returns the number of white pieces of a certain type
     */
    public int White(PieceType type)
    {
        return whiteCounter[type];
    }
    
    /**
     * Returns the number of black pieces of a certain type
     */
    public int Black(PieceType type)
    {
        return blackCounter[type];
    }
}

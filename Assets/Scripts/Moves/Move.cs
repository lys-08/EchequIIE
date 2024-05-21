using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Move //: MonoBehaviour
{
    public abstract MoveType Type { get; }
    public abstract Position FromPos { get; } // Position where the piece move from
    public abstract Position ToPos { get; } // Position where the piece move to

    /**
     * Execute itself on the board
     * -> like the command pattern
     */
    public abstract void Execute(Board board);
}

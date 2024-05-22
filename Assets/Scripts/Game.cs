using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player 
{
    None,
    White, 
    Black
}

public class Game : MonoBehaviour
{
    public Board Board { get; }
    public Player CurrentPLayer { get; private set; }

    public Game(Player player, Board board)
    {
        CurrentPLayer = player;
        Board = board;
    }
}
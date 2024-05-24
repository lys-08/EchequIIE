using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI reasonText;
    private Game game;
    
    public void Awake()
    {
        game = FindObjectOfType<Game>();
        Result result = game.Result;
        winnerText.text = GetWinnerText(result.Winner);
        reasonText.text = GetReasonText(result.Reason, game.CurrentPlayer);
    }

    /**
     * Returns the Winner text : the header of the window
     */
    private string GetWinnerText(Player winner)
    {
        return winner switch
        {
            Player.White => "WHITE WINS !",
            Player.Black => "BLACK WINS !",
            _ => "IT'S A DRAW"
        };
    }

    /**
     * Returns the player string
     */
    private string PlayerString(Player player)
    {
        return player switch
        {
            Player.White => "WHITE",
            Player.Black => "BLACK",
            _ => ""
        };
    }

    /**
     * Returns text explaining the reason of the game over
     */
    private string GetReasonText(EndReason reason, Player currentPlayer)
    {
        return reason switch
        {
            EndReason.Stalemate => $"STALEMATE - {PlayerString(currentPlayer)} can't move",
            EndReason.Checkmate => $"CHECKMATE - {PlayerString(currentPlayer)} can't move",
            EndReason.FiftyMoveRule => "FIFTY-MOVE RULE",
            EndReason.InsufficientMaterial => "INSUFFICIENT MATERIAM",
            EndReason.ThreefoldRepetition => "THREEFOLD REPETITION",
            _ => ""
        };
    }

    /**
     * Change the scene to the main menu scene
     */
    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

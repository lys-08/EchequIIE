using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EndReason
{
    Checkmate,
    Stalemate,
    FiftyMoveRule,
    InsufficientMaterial,
    ThreefoldRepetition
}
public class Result
{
    public Player Winner { get; }
    public EndReason Reason { get; }

    public Result(Player winner, EndReason reason)
    {
        Winner = winner;
        Reason = reason;
    }
    
    public static Result Win(Player player)
    {
        return new Result(player, EndReason.Checkmate);
    }

    public static Result Draw(EndReason reason)
    {
        return new Result(Player.None, EndReason.Checkmate);
    }
}

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class ExampleTouch : MonoBehaviour
{
    private Game game;
    private int row = -1;
    private int col = -1;


    public void SetPosition(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Construct a ray from the current touch coordinates
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // Create a particle if hit
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("hit " + gameObject.name);
                    
                    Position pos = new Position(row, col);
                    Piece piece = game.Board[pos].GetComponentInChildren<Piece>();
                    if (piece == null)
                    {
                        Debug.Log("nothing");
                        game.OnToPositionSelected(pos);
                    }
                    else
                    {
                        Debug.Log("piece");
                        game.OnFromPositionSelected(pos);
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public Tile upNeighbour;
    public Tile downNeighbour;
    public Tile leftNeighbour;
    public Tile rightNeighbour;

    public Vector2Int index;


    public enum TileState {
        EMPTY, X, O
    }

    public TileState state;

    public Tile() {
        upNeighbour = null;
        downNeighbour = null;
        leftNeighbour = null;
        rightNeighbour = null;
        index = new Vector2Int(0, 0);
        state = TileState.EMPTY;
    }

    public void PrintStatus() {
        Debug.Log("Tile index" + index+" "+state);

       /* if (upNeighbour!=null) Debug.Log("Has up N");
        if (downNeighbour != null) Debug.Log("Has down S");
        if (leftNeighbour != null) Debug.Log("Has left W");
        if (rightNeighbour != null) Debug.Log("Has right E");
    */}

}

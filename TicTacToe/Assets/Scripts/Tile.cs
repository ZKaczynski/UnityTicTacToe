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

    public Tile(Vector2Int vec) {
        upNeighbour = null;
        downNeighbour = null;
        leftNeighbour = null;
        rightNeighbour = null;
        index = vec;
        state = TileState.EMPTY;
    }

    public void PrintStatus() {
        Debug.Log("Tile index->" + index+"Status->"+state);
    }

    public Tile Copy() {
        Tile t = new Tile(index) {
            upNeighbour = upNeighbour,
            downNeighbour = downNeighbour,
            leftNeighbour = leftNeighbour,
            rightNeighbour = rightNeighbour,
            state = state
        };
        return t;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    public int width;
    public int height;

    public enum TileState {
        EMPTY, X, O
    }
    public struct move {
        public TileState state;
        public int x;
        public int y;
    }


    public  TileState[] states;

    void Initialize(){
        states = new TileState[width*height];
        for (int i = 0; i < width * height; i++) {
            states[i] = TileState.EMPTY;
        }
    }

    /*
    void MakeMove(move mov){
        states[mov.x][mov.y] = mov.state;
    }
    */

    bool IsGameOver() {
        bool isOver = false;
        for (int i=0; i < width; i++) {
            TileState stateToCompare = states[i*height];
          //  for(int j=i*height+1;j<i*height+)
        }
      
    }

}

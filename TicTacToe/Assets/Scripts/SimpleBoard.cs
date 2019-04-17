using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoard 
{
    public enum Tile {
        X,O,EMPTY
    }
    public enum State {
        XWON, OWON, DRAW, UNDECIDED
    }
    public bool turn; 
    public Tile[] TableBoard;
    public State state;
    public SimpleBoard() {
        state = State.UNDECIDED;
        turn = true;
        TableBoard = new Tile[9];
        for (int i = 0; i < 9; i++) {
            TableBoard[i] = Tile.EMPTY;
        }
    }
    public void Next() {
        turn = !turn;
    }
    public bool IsEnd() {
        return (state != State.UNDECIDED);
    }
    public void Evaluate() {
        if (TableBoard[0] == Tile.X && TableBoard[1] == Tile.X && TableBoard[2] == Tile.X ||
            TableBoard[3] == Tile.X && TableBoard[4] == Tile.X && TableBoard[5] == Tile.X ||
            TableBoard[6] == Tile.X && TableBoard[7] == Tile.X && TableBoard[8] == Tile.X ||
            TableBoard[0] == Tile.X && TableBoard[3] == Tile.X && TableBoard[6] == Tile.X ||
            TableBoard[1] == Tile.X && TableBoard[4] == Tile.X && TableBoard[7] == Tile.X ||
            TableBoard[2] == Tile.X && TableBoard[5] == Tile.X && TableBoard[8] == Tile.X ||
            TableBoard[0] == Tile.X && TableBoard[4] == Tile.X && TableBoard[8] == Tile.X ||
            TableBoard[2] == Tile.X && TableBoard[4] == Tile.X && TableBoard[6] == Tile.X
            ) {
            state = State.XWON;
        } else if (TableBoard[0] == Tile.O && TableBoard[1] == Tile.O && TableBoard[2] == Tile.O ||
                TableBoard[3] == Tile.O && TableBoard[4] == Tile.O && TableBoard[5] == Tile.O ||
                TableBoard[6] == Tile.O && TableBoard[7] == Tile.O && TableBoard[8] == Tile.O ||
                TableBoard[0] == Tile.O && TableBoard[3] == Tile.O && TableBoard[6] == Tile.O ||
                TableBoard[1] == Tile.O && TableBoard[4] == Tile.O && TableBoard[7] == Tile.O ||
                TableBoard[2] == Tile.O && TableBoard[5] == Tile.O && TableBoard[8] == Tile.O ||
                TableBoard[0] == Tile.O && TableBoard[4] == Tile.O && TableBoard[8] == Tile.O ||
                TableBoard[2] == Tile.O && TableBoard[4] == Tile.O && TableBoard[6] == Tile.O
                ) {
            state = State.OWON;
        } else {
            int count = 0;
            for (int i = 0; i < 9; i++) {
                if (TableBoard[i] != Tile.EMPTY) {
                    count++;
                }
                if (count == 9) state = State.DRAW;
                else state = State.UNDECIDED;
            }
        }
    }
    public void MakeMove(int move) {
        if (turn) {
            TableBoard[move] = Tile.X;
        }
        else {
            TableBoard[move] = Tile.O;
        }
        Evaluate();
        Next();
    }

    public void UnDoMove(int move) {
        TableBoard[move] = Tile.EMPTY;
        Evaluate();
        Next();
    }

    public List<int> GetMoves() {
        List<int> possibleMoves = new List<int>();
        for (int i = 0; i < 9; i++) {
            if (TableBoard[i] == Tile.EMPTY) {
                possibleMoves.Add(i);
            }
        }
        return possibleMoves;
    }

    public bool IsMoveValid (int move) {
        return (TableBoard[move] == Tile.EMPTY);

    }

    public void show() {
        int count = 0;
        for (int i = 0; i < 9; i++) {
            if (TableBoard[i] != Tile.EMPTY) {
                 Debug.Log(TableBoard[i]);
                count++;
            }
        }
        Debug.Log(count);
    }
}

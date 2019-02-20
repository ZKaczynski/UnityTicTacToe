using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    private int width;
    private int height;

    public Tile[,] boardTiles;

    public enum Player {
        XPlayer,OPlayer
    }


    Player currentPlayer;

    public Board(int w, int h, Player p) {
        width = w;
        height = h;
        currentPlayer = p;
        boardTiles = new Tile[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                boardTiles[i, j] = new Tile();
            }
        }
        ConnectTiles();
    }

    public List<Vector2Int> GetMoves() {
        List<Vector2Int> possibleMoves = new List<Vector2Int>();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (boardTiles[i, j].state == Tile.TileState.EMPTY) {
                    possibleMoves.Add(new Vector2Int(i, j));
                }
            }
        }
        return possibleMoves;
    }

    public void MakeMove(Vector2Int vec) {
        if (currentPlayer == Player.XPlayer)
            boardTiles[vec.x, vec.y].state = Tile.TileState.X;
        else boardTiles[vec.x, vec.y].state = Tile.TileState.O;

        NextPlayer();
    }

    public int Evaluate() { //TODO
        for (int i = 0; i < width; i++) {
            if (boardTiles[i, 0].state != Tile.TileState.EMPTY) {
                Tile firstTile = boardTiles[i, 0];
                for (int k = 1; k < height; k++) {
                    if (firstTile.state != boardTiles[i, k].state) break;
                    if (k == height - 1) return 10;
                }
            }
        }
        for (int i = 0; i < height; i++) {
            if (boardTiles[0, i].state != Tile.TileState.EMPTY) {
                Tile firstTile = boardTiles[0, i];
                for (int k = 1; k < width; k++) {
                    if (firstTile.state != boardTiles[k, i].state) break;
                    if (k == width - 1) return 10;
                }
            }
        }

        return 0;
    }

    public Player CurrentPlayer() {
        return currentPlayer;
    }

    public bool IsGameOver() {
        for (int i = 0; i < width; i++) {
           if (boardTiles[i, 0].state != Tile.TileState.EMPTY) {
                Tile firstTile = boardTiles[i, 0];
                for (int k = 1; k < height; k++) {
                    if (firstTile.state != boardTiles[i, k].state) break;
                    if (k == height-1) return true;
                }
            }
        }
        for (int i = 0; i < height; i++) {
            if (boardTiles[0, i].state != Tile.TileState.EMPTY) {
                Tile firstTile = boardTiles[0, i];
                for (int k = 1; k < width; k++) {
                    if (firstTile.state != boardTiles[k, i].state) break;
                    if (k == width - 1) return true;
                }
            }
        }
        return false;
    }

   

    public void PrintBoard() {
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                boardTiles[i, j].PrintStatus();
            }
        }
    }
    private void NextPlayer() {
        if (currentPlayer == Player.OPlayer) currentPlayer = Player.XPlayer;
        else currentPlayer = Player.OPlayer;
    }

    private void ConnectTiles() {

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                boardTiles[i, j].index = new Vector2Int(i, j);

                if (i != width - 1) {
                    boardTiles[i, j].rightNeighbour = boardTiles[i + 1, j];
                }
                if (i != 0) {
                    boardTiles[i, j].leftNeighbour = boardTiles[i - 1, j];
                }
                if (j != height - 1) {
                    boardTiles[i, j].upNeighbour = boardTiles[i, j + 1];
                }
                if (j != 0) {
                    boardTiles[i, j].downNeighbour = boardTiles[i, j - 1];
                }

            }
        }
    }

    public void UndoMove(Vector2Int vec) {
        boardTiles[vec.x, vec.y].state = Tile.TileState.EMPTY;
        NextPlayer();
    }

    /*
    public Board Copy() {
        Board b = new Board(width, height,currentPlayer);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                b.boardTiles[i,j]=
            }
        }
        return b;
    }*/
}

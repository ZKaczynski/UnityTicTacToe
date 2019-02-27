using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    private readonly int width;
    private readonly int height;

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
                boardTiles[i, j] = new Tile(new Vector2Int(i,j));
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

    public int Evaluate() {

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


    public int Evaluate(Player p) {
        Tile.TileState myState;
        Tile.TileState opponentState; ;
        if (p == Player.XPlayer) {
            myState = Tile.TileState.X;
            opponentState = Tile.TileState.O;
        }
        else {
            myState = Tile.TileState.O;
            opponentState = Tile.TileState.X;
        }

        
        for (int i = 0; i < width; i++) {
            if (boardTiles[i, 0].state == myState) {
                Tile firstTile = boardTiles[i, 0];
                for (int k = 1; k < height; k++) {
                    if (firstTile.state != boardTiles[i, k].state) break;
                    if (k == height - 1) return 10;
                }
            }
            else if (boardTiles[i, 0].state == opponentState) {
                Tile firstTile = boardTiles[i, 0];
                for (int k = 1; k < height; k++) {
                    if (firstTile.state != boardTiles[i, k].state) break;
                    if (k == height - 1) return -10;
                }
            }

        }
        for (int i = 0; i < height; i++) {
            if (boardTiles[0, i].state == myState) {
                Tile firstTile = boardTiles[0, i];
                for (int k = 1; k < width; k++) {
                    if (firstTile.state != boardTiles[k, i].state) break;
                    if (k == width - 1) return 10;
                }
            }
            else if (boardTiles[0, i].state == opponentState) {
                Tile firstTile = boardTiles[0, i];
                for (int k = 1; k < width; k++) {
                    if (firstTile.state != boardTiles[k, i].state) break;
                    if (k == width - 1) return -10;
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
        int numberOfTiles = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (boardTiles[i, j].state != Tile.TileState.EMPTY) numberOfTiles++;
                else return false;
            }
        }
        if (numberOfTiles == width * height) return true;

        return false;
    }

   public void UndoMove(Vector2Int vec) {
        boardTiles[vec.x, vec.y].state = Tile.TileState.EMPTY;
        NextPlayer();
    }

    public void PrintBoard() {
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                boardTiles[i, j].PrintStatus();
            }
        }
    }
    public void NextPlayer() {
        if (currentPlayer == Player.OPlayer) currentPlayer = Player.XPlayer;
        else currentPlayer = Player.OPlayer;
    }

    private void ConnectTiles() {

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

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

    public Board Copy() {
        Board b = new Board(width, height, currentPlayer);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                b.boardTiles[i, j] = boardTiles[i, j].Copy();
            }
        }
        return b;
    }
}

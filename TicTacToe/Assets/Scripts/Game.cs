using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board mainBoard;
    private Vector2Int bestMove;

    public int width;
    public int height;

    public Squere squerePrefab;
    Squere[,] squeres;
    private float interval = 2.0f;
    public Sprite empty;
    public Sprite x;
    public Sprite o;


    void Start() {

        squeres = new Squere[width,height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                CreateTile(x, y);
            }
        }

        mainBoard = new Board(width, height,Board.Player.XPlayer);

        for (int i = 0; i < 9; i++) {
            Negamax(mainBoard, 10, 0);
            Debug.Log(bestMove);
            mainBoard.MakeMove(bestMove);

            if (mainBoard.CurrentPlayer() == Board.Player.XPlayer) {
                ChanheSprite(bestMove, o);
            }
            else {
                ChanheSprite(bestMove, x);
            }



            mainBoard.PrintBoard();

        }

    }



    void CreateTile(int x, int y) {
        Vector2 position;
        position.x = (x - width / 2) * interval;
        position.y = (y - height / 2) * interval;
        squeres[x,y] = Instantiate<Squere>(squerePrefab);
        squeres[x,y].transform.SetParent(transform, false);
        squeres[x,y].transform.localPosition = position;
    }

    void ChanheSprite(Vector2Int vec, Sprite s) {
        squeres[vec.x, vec.y].GetComponent<SpriteRenderer>().sprite = s;
    }





    private int Negamax(Board board, int maxDepth, int currentDepth) {
        if (board.IsGameOver() || currentDepth == maxDepth)
            return board.Evaluate();

        int bestScore = -1000;

        foreach(Vector2Int move in board.GetMoves()) {
            int recursedScore = 0;
            board.MakeMove(move);
            recursedScore = Negamax(board,maxDepth,currentDepth++);
            board.UndoMove(move);
            int currentScore = -recursedScore;
            if (currentScore > bestScore) {
                bestScore = currentScore;
                bestMove = move;
            }
        }
        return bestScore;
    }

    /*
    public Tile tilePrefab;
    public GameObject focusPrefab;

    GameObject focus;
    Tile[] tiles;



    
            focus = Instantiate<GameObject>(focusPrefab);
        focus.transform.SetParent(transform, false);
        focus.transform.localPosition = new Vector2(0.0f, 0.0f);




    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            focus.transform.localPosition = focus.transform.localPosition + new Vector3(-interval, 0.0f, 0.0f);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            focus.transform.localPosition = focus.transform.localPosition + new Vector3(interval, 0.0f, 0.0f);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            focus.transform.localPosition = focus.transform.localPosition + new Vector3(0.0f, interval, 0.0f);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            focus.transform.localPosition = focus.transform.localPosition + new Vector3(0.0f, -interval, 0.0f);

        }

    }*/
}

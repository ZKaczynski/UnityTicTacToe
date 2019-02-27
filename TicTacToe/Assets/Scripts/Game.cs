using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    private Board mainBoard;
   // private Vector2Int bestMove;

    public int width;
    public int height;

    public Squere squerePrefab;
    Squere[,] squeres;
    private float interval = 2.0f;
    public Sprite empty;
    public Sprite x;
    public Sprite o;

    private struct scoreAndMove{
        public Vector2Int move;
        public int score;

        public scoreAndMove(int s,Vector2Int vec ) {
            move = vec;
            score = s;
        }

    }

   


    void Start() {

        squeres = new Squere[width,height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                CreateTile(x, y);
            }
        }

        mainBoard = new Board(width, height,Board.Player.XPlayer);
        /*
        mainBoard.MakeMove(new Vector2Int(0, 0));
        mainBoard.MakeMove(new Vector2Int(1, 1));
        mainBoard.MakeMove(new Vector2Int(1,0));
        mainBoard.MakeMove(new Vector2Int(2, 2));
        mainBoard.MakeMove(new Vector2Int(2, 1));
        */
        Debug.Log(mainBoard.IsGameOver());
        Debug.Log(mainBoard.Evaluate(Board.Player.XPlayer));
        //scoreAndMove sam = Minimax(mainBoard, mainBoard.CurrentPlayer(), 9, 0);
        scoreAndMove sam = Negamax(mainBoard, 9, 0);
        Debug.Log(sam.move + "||" + sam.score);
        /*
        for (int i = 0; i < 5; i++) {
            // Negamax(mainBoard, 10, 0);
            scoreAndMove sam = Minimax(mainBoard, mainBoard.CurrentPlayer(), 9, 0);
            Debug.Log("Best move"+sam.move);
            

            if (mainBoard.CurrentPlayer() == Board.Player.XPlayer) {
                ChanheSprite(sam.move, o);
            }
            else {
                ChanheSprite(sam.move, x);
            }
            mainBoard.MakeMove(sam.move);

            // bestMove = new Vector2Int(-1,-1);


            mainBoard.PrintBoard();

        }
        */
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
        Debug.Log(vec);
        squeres[vec.x, vec.y].GetComponent<SpriteRenderer>().sprite = s;
    }


    private scoreAndMove Minimax(Board board, Board.Player player, int maxDepth,int currentDepth) {
        if (board.IsGameOver() || currentDepth == maxDepth) {
            Debug.Log("Bottom of recursion");
            return new scoreAndMove(board.Evaluate(player), new Vector2Int(0, 0));
        }
       
        int bestScore;
        if (board.CurrentPlayer() == player) bestScore = -1000;
        else bestScore = 1000;
        Vector2Int bestMove= new Vector2Int(-1,-1);

        int k = 0;
        foreach (Vector2Int move in board.GetMoves()) {  
           // Board newBoard =  //board.Copy();
            Debug.Log("Inner minimax "+k+" "+currentDepth+" "+move);
            k++;
            board.MakeMove(move);
            scoreAndMove sam;

            sam = Minimax(board, player, maxDepth, currentDepth+1);
            
            if (board.CurrentPlayer() == player) {
                if (sam.score > bestScore) {
                    bestScore = sam.score;
                    bestMove = sam.move;
                }
            }
            else {
                if (sam.score < bestScore) {
                    bestScore = sam.score;
                    bestMove = sam.move;
                }
            }
            board.UndoMove(move);   
        }
        return new scoreAndMove(bestScore, bestMove);
    }


    private scoreAndMove Negamax(Board board, int maxDepth, int currentDepth) {
        if (board.IsGameOver() || currentDepth == maxDepth) {
            Debug.Log("Bottom");
            return new scoreAndMove(board.Evaluate(), new Vector2Int(-1, -1));
        }

        scoreAndMove bestChoice = new scoreAndMove(-1000, new Vector2Int(-1, -1));

        foreach(Vector2Int move in board.GetMoves()) {
            board.MakeMove(move);
            scoreAndMove sac = Negamax(board,maxDepth,currentDepth++);
            board.UndoMove(move);
            sac.score = -sac.score;
            if (sac.score > bestChoice.score) {
                bestChoice.score = sac.score;
                bestChoice.move = sac.move;
            }
        }
        return bestChoice;
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

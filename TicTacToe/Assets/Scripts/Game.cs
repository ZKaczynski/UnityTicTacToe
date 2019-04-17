using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    private SimpleBoard mainBoard;
    public int width;
    public int height;
    public Squere squerePrefab;
    Squere[,] squeres;
    private float interval = 2.0f;
    public Sprite empty;
    public Sprite x;
    public Sprite o;
    public Image currentPlayerImage;
    public Text currentPlayerText;
    public Button XSetter;
    public Text XSetterText;
    private bool xIsAi;
    private bool oIsAi;

    private struct ScoreAndMove {
        public int move;
        public int score;
        public ScoreAndMove(int s, int m) {
            move = m;
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
        mainBoard = new SimpleBoard();
        xIsAi = true;
        oIsAi = true;
        currentPlayerImage.GetComponent<Image>().sprite = x;
    }
    public void SetXAi() {
        xIsAi = !xIsAi;
        if (xIsAi) {
            XSetterText.text = "Change X to Human";
        } else {
            XSetterText.text = "Change X to AI";
        }
    }
    public void SetOAi() {
        oIsAi = !oIsAi;
    }

    private void ExecuteMove(int move) {
        if (mainBoard.IsEnd()) {
            if (mainBoard.state == SimpleBoard.State.DRAW) {
                currentPlayerText.text = "IT IS A DRAW!";
            } else if (mainBoard.state == SimpleBoard.State.XWON) {
                currentPlayerText.text = "X HAS WON!";
            } else if (mainBoard.state == SimpleBoard.State.OWON) {
                currentPlayerText.text = "O HAS WON!";
            }
        }

        else if (mainBoard.turn && !xIsAi && mainBoard.IsMoveValid(move)) {
            ChangeSprite(move, x);
            mainBoard.MakeMove(move);
            currentPlayerImage.GetComponent<Image>().sprite = o;
        }


    }




    private void Update() {
        if(Input.GetKeyDown(KeyCode.Keypad1)) {
            ExecuteMove(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            ExecuteMove(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            ExecuteMove(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            ExecuteMove(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            ExecuteMove(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6)) {
            ExecuteMove(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            ExecuteMove(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8)) {
            ExecuteMove(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9)) {
            ExecuteMove(8);
        }

        if (Input.GetKeyDown(KeyCode.Space)&&
            ((mainBoard.turn && xIsAi) || (!mainBoard.turn && oIsAi))) {
            if (!mainBoard.IsEnd()) {

                ScoreAndMove sam = Negamax(mainBoard, 0);

                int move = sam.move;

               
                if (!mainBoard.turn) {
                    ChangeSprite(move, o);
                    currentPlayerImage.GetComponent<Image>().sprite = x;
                } else {
                    ChangeSprite(move, x);
                    currentPlayerImage.GetComponent<Image>().sprite = o;
                }
                mainBoard.MakeMove(move);
            } else {
                if (mainBoard.state == SimpleBoard.State.DRAW) {
                    currentPlayerText.text = "IT IS A DRAW!";
                }
                else if (mainBoard.state == SimpleBoard.State.XWON) {
                    currentPlayerText.text = "X HAS WON!";
                }
                else if (mainBoard.state == SimpleBoard.State.OWON) {
                    currentPlayerText.text = "O HAS WON!";
                }
            }
        }
    }

    private ScoreAndMove Negamax(SimpleBoard simp, int currentDepth) {
        if (simp.state == SimpleBoard.State.DRAW) return new ScoreAndMove(0, -1);
        else if (simp.state == SimpleBoard.State.XWON|| simp.state == SimpleBoard.State.OWON) return new ScoreAndMove(10-currentDepth, -1);
        ScoreAndMove worst = new ScoreAndMove(1000, -1);
        foreach (int move in simp.GetMoves()) {
            simp.MakeMove(move);
            ScoreAndMove sam = Negamax(simp, currentDepth + 1);
            simp.UnDoMove(move);
            sam.score = -sam.score;
            if (sam.score < worst.score) {
                worst.score = sam.score;
                worst.move = move;
            }
        }
        return worst;
    }

    void CreateTile(int x, int y) {
        Vector2 position;
        position.x = (x - width / 2) * interval;
        position.y = (y - height / 2) * interval;
        squeres[x,y] = Instantiate<Squere>(squerePrefab);
        squeres[x,y].transform.SetParent(transform, false);
        squeres[x,y].transform.localPosition = position;
    }

    void ChangeSprite(int num, Sprite s) {
        if (num == 0) {
            squeres[0, 0].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 1) {
            squeres[1, 0].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 2) {
            squeres[2, 0].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 3) {
            squeres[0, 1].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 4) {
            squeres[1, 1].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 5) {
            squeres[2, 1].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 6) {
            squeres[0, 2].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 7) {
            squeres[1, 2].GetComponent<SpriteRenderer>().sprite = s;
        }
        if (num == 8) {
            squeres[2, 2].GetComponent<SpriteRenderer>().sprite = s;
        }
    }
}

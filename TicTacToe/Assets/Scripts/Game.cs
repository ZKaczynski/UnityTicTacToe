using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board board;
    public GameObject tilePrefab;
    public int  width;
    public int height;
     

    public GameObject focusPrefab;

    GameObject focus;
    GameObject[] tiles;

    public Sprite X;
    public Sprite O;

    private float interval=2.0f;

    void Awake(){

       tiles = new GameObject[width * height];
       for (int x = 0,i=0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                CreateTile(x,y,i++);
            }
        }
       focus = Instantiate<GameObject>(focusPrefab);
       focus.transform.SetParent(transform, false);
       focus.transform.localPosition = new Vector2(0.0f,0.0f);
    }



    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
           focus.transform.localPosition = focus.transform.localPosition + new Vector3(-interval, 0.0f,0.0f);

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

    }

    void CreateTile(int x,int y,int i) {
        Vector2 position;
        position.x = (x-width/2) * interval;
        position.y = (y-height/2) * interval;
        tiles[i] = Instantiate<GameObject>(tilePrefab);
        tiles[i].transform.SetParent(transform, false);
        tiles[i].transform.localPosition = position;
    }

    void setTile() {

    }

}

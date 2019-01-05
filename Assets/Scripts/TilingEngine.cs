using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean;
using Lean.Pool;

public class TilingEngine : MonoBehaviour
{
    public Vector2 MapSize;
    public List<BoardTileSprite> TileSprites;
    public Sprite DefaultImage;
    public GameObject TileContainerPrefab;
    public GameObject TilePrefab;
    public Vector2 CurrentPosition;
    public int tilePixelsLength;
    public int numRandomSpecialTiles;
    private BoardTileSprite[,] _map;
    private GameObject controller;
    private GameObject _tileContainer;
    private List<GameObject> _tiles = new List<GameObject>();


    private BoardTileSprite FindTile(BoardTiles tile)
    {
        foreach (BoardTileSprite tileSprite in TileSprites)
        {
            if (tileSprite.TileType == tile) return tileSprite;
        }
        return null;
    }

    private void setDefaultTiles()
    {
        for (var i = 0; i < TileSprites.Count; i++) {
            print("numbah " + i + " and name " + TileSprites[i].Name);
        }

        for (int y = 0; y < MapSize.y; y++) {
            for (int x = 0; x < MapSize.x; x++) {
                if ((x % (int)(MapSize.x / 2) == 0) && (y % (int)(MapSize.y / 2) == 0)) {
                    _map[x, y] = new BoardTileSprite(TileSprites[3].Name, TileSprites[3].TileImage, TileSprites[3].TileType);
                }
                else if ((x > 0 && x < MapSize.x - 1 && y > 0 && y < MapSize.y - 1) && 
                         ((x == y) || ((int)MapSize.x - x - 1 == y))) {
                    _map[x, y] = new BoardTileSprite(TileSprites[1].Name, TileSprites[1].TileImage, TileSprites[1].TileType);
                }
                else {
  //                  print(x + " , " + y + "blank spot here");
                    _map[x, y] = new BoardTileSprite(TileSprites[0].Name, TileSprites[0].TileImage, TileSprites[0].TileType);
                }
            }
        }
    }

    private void setTilesRandom()
    {
        var index = 0;
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                _map[x, y] = new BoardTileSprite(TileSprites[index].Name, TileSprites[index].TileImage, TileSprites[index].TileType);
                index++;
                if (index > TileSprites.Count - 1) index = 0;
            }
        }


    }



    private void myAddTiles()
    {
        foreach (GameObject o in _tiles)
        {
            LeanPool.Despawn(o);
        }
        _tiles.Clear();
        LeanPool.Despawn(_tileContainer);
        _tileContainer = LeanPool.Spawn(TileContainerPrefab);

        Vector2 camPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        for (int y = 0; y < MapSize.y; y++)
        {
            for (int x = 0; x < MapSize.x; x++)
            {
                var yActual = y - (MapSize.y / 2) + 1;
                var xActual = x - (MapSize.x / 2) + 1;
//                print("x, y : " + xActual + ", " + yActual);

                var t = LeanPool.Spawn(TilePrefab);
                t.transform.position = new Vector3(xActual, yActual, 0);
                t.transform.SetParent(_tileContainer.transform);
                var rend = t.GetComponent<SpriteRenderer>();
                rend.sprite = _map[(int)x, (int)y].TileImage;
                _tiles.Add(t);
            }
        }
    }



    public void Start()
    {
        controller = GameObject.Find("Controller");
        _map = new BoardTileSprite[(int)MapSize.x, (int)MapSize.y];

        setDefaultTiles();
     //   setTilesRandom();
        myAddTiles();
    }

    private void Update()
    {

    }
}


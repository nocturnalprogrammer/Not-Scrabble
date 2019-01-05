using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class BoardTileSprite
{
    public string Name;
    public Sprite TileImage;
    public BoardTiles TileType;

    public BoardTileSprite(Sprite image)
    {
        Name = "Blank";
        TileImage = image;
        TileType = BoardTiles.Blank;
    }

    public BoardTileSprite(string name, Sprite image, BoardTiles tile)
    {
        Name = name;
        TileImage = image;
        TileType = tile;
    }
}
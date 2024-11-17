using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public enum BlockType
{
    Creating,
    Ground,
    Wall,
    Goal,
    Structure,
    Water
}
public class MapManager : MonoBehaviour
{
    //プレイヤー
    [SerializeField]
    GameObject player;
    //マスターデータ
    [SerializeField]
    MasterDatas masterData;
    Biomes biomes;
    
    //マップの生成サイズの最大最小
    [SerializeField]
    int sizeMin;
    [SerializeField]
    int sizeMax;
    //生成されたマップのサイズ
    Vector2Int mapSize;
    //マップの左上の座標
    Vector2 mapLeftTop;
    //マップの右下の座標
    Vector2 mapRightBottom;
    //タイルマップ
    [SerializeField]
    Tilemap tileMap_Wall;
    [SerializeField]
    Tilemap tileMap_Ground;
    [SerializeField]
    Tilemap tileMap_Structures;
    [SerializeField]
    Tilemap tileMap_Structures_Collider;

    //デフォルトのタイル
    public TileBase ground;
    public TileBase wall;
    public TileBase goal;
    //マップデータ
    List<List<MapData>> mapDatas = new List<List<MapData>>();

    public MapData GetMapData(Vector2 vec)
    {
        Vector2Int mapPos = GetMapPos(vec);
        return mapDatas[mapPos.y][mapPos.x];
    }
    public Vector2 GetLeftTop()
    {
        return mapLeftTop;
    }
    public Vector2 GetRightBottom()
    {
        return mapRightBottom;
    }
    void Awake()
    {
        biomes = masterData.biomeData;
    }
    void Start()
    {
        GeneratorMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratorMap()
    {
        do
        {
            mapSize.x = Random.Range(sizeMin, sizeMax);
            mapSize.y = Random.Range(sizeMin, sizeMax);
        } while (mapSize.x % 2 != 0 || mapSize.y % 2 != 0);

        Vector2 halfMapSize = mapSize / 2;
        mapLeftTop = new Vector2(-halfMapSize.x, halfMapSize.y);
        mapRightBottom = new Vector2(halfMapSize.x, -halfMapSize.y);
        Debug.Log("サイズは" + mapSize);
        Debug.Log("左上に座標は" + mapLeftTop + "。右下の座標は" + mapRightBottom);

        for (int i = 0; i < mapSize.y; i++)
        {
            mapDatas.Add(new List<MapData>());
            for (int j = 0; j < mapSize.x; j++)
            {
                mapDatas[i].Add(new MapData(BlockType.Wall,wall));
            }
        }

        Vector2Int startPos = new Vector2Int(Random.Range(0,mapSize.x - 1), Random.Range(0,mapSize.y - 1));
        player.transform.position = GetWorldPos(startPos + (Vector2)tileMap_Ground.tileAnchor);

        GenerateBiome("cave", startPos);

        GetStartToEndPath();
        
        int newRoomCount = Random.Range(10,15);
        
        for (int i = 0;i < newRoomCount; i++)
        {
            int roomCoreX = Random.Range(0, mapSize.x);
            int roomCoreY = Random.Range(0, mapSize.y);

            int rnd = Random.Range(0, 100);
            Vector2Int biomeCore = new Vector2Int(roomCoreX, roomCoreY);
            if(rnd < 60)
            {
                GenerateBiome("cave", biomeCore);
            }
            else
            {
                GenerateBiome("ocean", biomeCore);
            }
        }

        for (int i = 0; i < mapDatas.Count; i++)
        {
            for (int j = 0; j < mapDatas[i].Count; j++)
            {
                Vector2Int drawCellPos = new Vector2Int(j, i);
                switch (mapDatas[i][j].blockType)
                {
                    case BlockType.Ground:
                    case BlockType.Structure:
                        SetTile(tileMap_Ground, drawCellPos, mapDatas[i][j]);
                        break;
                    case BlockType.Wall:
                        
                        if (CanSeemSurfaceWall(drawCellPos))
                        {
                            SetTile(tileMap_Wall, drawCellPos, mapDatas[i][j]);
                        }
                        else
                        {
                            SetTile(tileMap_Wall, drawCellPos, mapDatas[i][j]);
                        }

                        break;
                    case BlockType.Goal:
                        SetTile(tileMap_Ground, drawCellPos, mapDatas[i][j]);
                        break;
                    case BlockType.Water:

                        break;
                }
            }
        }
    }

    bool CanSeemSurfaceWall(Vector2Int pos)
    {
        Vector2Int[] aroundCells = new Vector2Int[]
        {
            pos + Vector2Int.up,
            pos + Vector2Int.down,
            pos + Vector2Int.right,
            pos + Vector2Int.left
        };
        foreach (Vector2Int aroundCell in aroundCells)
        {
            if (aroundCell.y < 0 || aroundCell.y >= mapDatas.Count) continue;
            if (aroundCell.x < 0 || aroundCell.x >= mapDatas[aroundCell.y].Count) continue;
            if (mapDatas[aroundCell.y][aroundCell.x].blockType == 0)
            {
                return true;
            }
        }
        return false;
    }
    void SetTile(Tilemap tilemap, Vector2 pos, MapData data)
    {
        Vector3Int tilePos = tilemap.WorldToCell(GetWorldPos(pos + (Vector2)tilemap.tileAnchor));
        tilemap.SetTile(tilePos, data.groundTile);
        if(data.structure != null)
        {
            if (data.structure.collider)
            {
                tilePos = tileMap_Structures_Collider.WorldToCell(GetWorldPos(pos + (Vector2)tilemap.tileAnchor));
                tileMap_Structures_Collider.SetTile(tilePos, data.structure.tile);
            }
            else
            {
                tilePos = tileMap_Structures.WorldToCell(GetWorldPos(pos + (Vector2)tilemap.tileAnchor));
                tileMap_Structures.SetTile(tilePos, data.structure.tile);
            }
        }
    }
    void GetStartToEndPath()
    {
        Vector2Int end = new Vector2Int(Random.Range(1,mapSize.x - 1),Random.Range(1,mapSize.y - 1));
        mapDatas[end.y][end.x].SetTile(BlockType.Goal,goal);

        GenerateBiome("cave", end);
    }
    public Vector2 GetWorldPos(Vector2 mapPos)
    {
        return new Vector2(mapLeftTop.x + mapPos.x, mapLeftTop.y - mapPos.y);
    }
    public Vector2Int GetMapPos(Vector2 worldPos)
    {
        return new Vector2Int((int)(worldPos.x - mapLeftTop.x), (int)(mapLeftTop.y - worldPos.y));
    }
    void GenerateBiome(string name,Vector2Int core)
    {
        Biome biome = biomes.GetBiome(name);
        biome.Generator(mapDatas, core, biome.groundTile, biome.wallTile);
    }
}

public class MapData
{
    public BlockType blockType;
    public TileBase groundTile;

    public Structure structure;

    public bool isExpanding;
    public void SetTile(BlockType type, TileBase tile,Structure structure = null)
    {
        this.blockType = type;
        this.groundTile = tile;
        this.structure = structure;
    }

    public MapData(BlockType type, TileBase tile)
    {
        this.blockType = type;
        this.groundTile = tile;
    }
}
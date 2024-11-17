using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BiomeGenerator : MonoBehaviour
{
    //マスターデータ
    public MasterDatas masterData;
    Biomes biomes;
    Structures structures;
    //参照データID
    public string biomeDataID;

    public void Awake()
    {
        structures = masterData.structureData;
        biomes = masterData.biomeData;
    }
    public Biome GetBiome(string name)
    {
       return biomes.GetBiome(name);
    }
    public Structure GetStructure(string name)
    {
        return structures.GetStructure(name);
    }
    //生成時処理
    public virtual void Create(List<List<MapData>> map, Vector2Int core, TileBase ground, TileBase wall){
        Debug.Log("マップの" + core + "を中心に" + GetBiome(biomeDataID).displayName +"を生成します。" + "床は" + ground.name + "、壁は" + wall.name + "です。");
        List<Vector2Int> rooms = new List<Vector2Int>()
        {
            core
        };
        while (true)
        {
            Vector2Int roomCell = rooms[0];

            Vector2Int[] lottyCore = new Vector2Int[]{
                    roomCell + Vector2Int.up,
                    roomCell + Vector2Int.down,
                    roomCell + Vector2Int.right,
                    roomCell + Vector2Int.left,
                };

            foreach (Vector2Int lotty in lottyCore)
            {
                if (ExpandBiome(50, map, lotty, ground))
                {
                    rooms.Add(lotty);
                }
            }
            rooms.RemoveAt(0);

            if (rooms.Count == 0) break;
        }
    }
    public virtual bool ExpandRule(List<List<MapData>> map)
    {
        return true;
    }
    public bool ExpandBiome(float expandProb, List<List<MapData>> map,Vector2Int pos,TileBase tile,BlockType blockType = BlockType.Ground)
    {
        if (pos.y < 1 || pos.y >= map.Count - 1) return false;
        if (pos.x < 1 || pos.x >= map[pos.y].Count - 1) return false;
        if (map[pos.y][pos.x].blockType != BlockType.Wall) return false;
        if(!ExpandRule(map)) return false;
        if (Random.Range(0, 100) >= expandProb) return false;

        Biome biome = GetBiome(biomeDataID);
        if (Random.Range(0.0f, 100.0f) < GetBiome(biomeDataID).structureFrequency)
        {
            GetStructure(biome.GetGenerateStructure()).Generator(map, pos, tile);
        }
        else
        {
            map[pos.y][pos.x].SetTile(blockType, tile);
        }
        return true;

    }
    private void OnEnable()
    {
        GetBiome(biomeDataID).SetGenerator(Create);
    }
    private void OnDisable()
    {
        GetBiome(biomeDataID).DisbleGenerator(Create);
    }
}

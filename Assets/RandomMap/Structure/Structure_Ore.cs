using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Structure_Ore : StructureGenerator
{
    //¶¬ƒpƒ^[ƒ“(‚à‚Á‚Æ‚í‚©‚è‚â‚·‚­‘‚«‚½‚¢)
    List<List<List<int>>> pattern = new List<List<List<int>>>()
    {
        new List<List<int>>
        {
            new List<int>{1}
        },
        new List<List<int>>
        {
            new List<int>{1,1},
            new List<int>{1,1}
        }
    };
    public override void Select(List<List<MapData>> map, Vector2Int core, Player player)
    {
        Debug.Log("zÎ‚ğˆê‚Âæ“¾‚µ‚Ü‚µ‚½");
        mapManager.DestroyStructure(core);
    }

    public override void Create(List<List<MapData>> map, Vector2Int core, TileBase structureTile, TileBase GroundTile)
    {
        int createPattern = Random.Range(0, pattern.Count);

        for(int i = 0; i < pattern[createPattern].Count; i++)
        {
            for(int j = 0; j < pattern[createPattern][i].Count; j++)
            {
                if (pattern[createPattern][i][j] == 1)
                {
                    map[core.y + i][core.x + j].SetTile(BlockType.Structure, GroundTile, GetStructure(structureDataID));
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

[Serializable]
public class Biome
{
    //参照用ID
    public string id;
    //表示名
    public string displayName;
    //地面として生成するタイル
    public TileBase groundTile;
    //壁として生成するタイル
    public TileBase wallTile;
    //生成される構造物のデータ
    public List<StructureGenerateData> structureData;
    //構造物が生成される頻度
    [Range(0.0f,100.0f)]
    public float structureFrequency;
    //生成時に使用される関数
    Action<List<List<MapData>>, Vector2Int, TileBase, TileBase> generator;
    public void SetGenerator(Action<List<List<MapData>>, Vector2Int, TileBase, TileBase> action)
    {
        generator += action;
    }
    public void DisbleGenerator(Action<List<List<MapData>>, Vector2Int, TileBase, TileBase> action)
    {
        generator -= action;
    }
    public void Generator(List<List<MapData>> map ,Vector2Int core, TileBase ground, TileBase wall)
    {
        generator.Invoke(map,core,ground,wall);
    }
    //構造物データの生成頻度の合計(生成時の計算に使用)
    public float GetTotalFrequency()
    {
        float total = 0;

        foreach(var data in structureData)
        {
            total += data.frequency;
        }
        return total;
    }
    public string GetGenerateStructure()
    {
        float prob = 0;
        float rnd = Random.Range(0, 100.0f);

        for (int i = 0; i < structureData.Count; i++)
        {
            prob += structureData[i].frequency / GetTotalFrequency();
            if(rnd < prob * 100.0f)
            {
                return structureData[i].id;
            }
        }
        return null;
    }
}
[Serializable]
public class StructureGenerateData
{
    //構造物参照用ID
    public string id;
    //生成頻度
    public int frequency;

    public StructureGenerateData(string name, int frequency)
    {
        this.id = name;
        this.frequency = frequency;
    }
}

[CreateAssetMenu(menuName = "ScriptableObject/Biomes")]
public class Biomes : ScriptableObject
{
    public Biome GetBiome(string id)
    {
        foreach(Biome biome in biomes)
        {
            if(biome.id == id) return biome;
        }
        return null;
    }
    [SerializeField]
    private List<Biome> biomes;
}

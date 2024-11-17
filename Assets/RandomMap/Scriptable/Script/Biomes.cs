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
    //�Q�ƗpID
    public string id;
    //�\����
    public string displayName;
    //�n�ʂƂ��Đ�������^�C��
    public TileBase groundTile;
    //�ǂƂ��Đ�������^�C��
    public TileBase wallTile;
    //���������\�����̃f�[�^
    public List<StructureGenerateData> structureData;
    //�\���������������p�x
    [Range(0.0f,100.0f)]
    public float structureFrequency;
    //�������Ɏg�p�����֐�
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
    //�\�����f�[�^�̐����p�x�̍��v(�������̌v�Z�Ɏg�p)
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
    //�\�����Q�ƗpID
    public string id;
    //�����p�x
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

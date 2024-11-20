using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Structure
{
    //参照用ID
    public string ID;
    //表示名
    public string displayName;
    //構造物の表示タイル
    public TileBase tile;
    //当たり判定の有無
    public bool collider;
    //選択した際の関数
    Action selectAction;
    //生成時の関数
    Action<List<List<MapData>>, Vector2Int, TileBase, TileBase> generator;
    public void SetGenerator(Action<List<List<MapData>>, Vector2Int, TileBase, TileBase> action)
    {
        generator += action;
    }
    public void DisbleGenerator(Action<List<List<MapData>>, Vector2Int, TileBase, TileBase> action)
    {
        generator -= action;
    }
    public void Generator(List<List<MapData>> map, Vector2Int core, TileBase ground)
    {
        generator.Invoke(map, core, tile,ground);
    }
    public void SetSelect(Action action)
    {
        selectAction += action;
    }
    public void DisbleSelect(Action action)
    {
        selectAction -= action;
    }
    public virtual void Select()
    {
        selectAction.Invoke();
    }
}

[CreateAssetMenu(menuName = "ScriptableObject/Structure")]
public class Structures : ScriptableObject
{
    public Structure GetStructure(string id)
    {
        foreach (Structure structure in structures)
        {
            if (structure.ID == id) return structure;
        }
        return null;
    }
    [SerializeField]
    private List<Structure> structures;
}
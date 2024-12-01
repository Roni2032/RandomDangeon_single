using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Structure
{
    //�Q�ƗpID
    public string ID;
    //�\����
    public string displayName;
    //�\�����̕\���^�C��
    public TileBase tile;
    //�����蔻��̗L��
    public bool collider;
    //�I�������ۂ̊֐�
    Action<List<List<MapData>>, Vector2Int, Player> selectAction;
    //�������̊֐�
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
    public void SetSelect(Action<List<List<MapData>>, Vector2Int, Player> action)
    {
        selectAction += action;
    }
    public void DisbleSelect(Action<List<List<MapData>>, Vector2Int, Player> action)
    {
        selectAction -= action;
    }
    public virtual void Select(List<List<MapData>> map, Vector2Int pos,Player player)
    {
        selectAction.Invoke(map,pos,player);
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
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureGenerator : MonoBehaviour
{
    //�}�X�^�[�f�[�^
    public MasterDatas masterData;
    Structures structures;

    protected MapManager mapManager;
    //�Q�ƃf�[�^ID
    public string structureDataID;
    public void Awake()
    {
        structures = masterData.structureData;
    }
    public Structure GetStructure(string name)
    {
        return structures.GetStructure(name);
    }
    //�I��(�\�����ɑ΂��ăN���b�N)�������̏���
    public virtual void Select(List<List<MapData>> map, Vector2Int core,Player player) { }
    //����������
    public virtual void Create(List<List<MapData>> map, Vector2Int core, TileBase structureTile, TileBase groundTile) { }

    private void OnEnable()
    {
        GetStructure(structureDataID).SetSelect(Select);
        GetStructure(structureDataID).SetGenerator(Create);

        mapManager = GameObject.FindGameObjectWithTag("Map").GetComponent<MapManager>();
    }
    private void OnDisable()
    {
        GetStructure(structureDataID).DisbleSelect(Select);
        GetStructure(structureDataID).DisbleGenerator(Create);
    }
}

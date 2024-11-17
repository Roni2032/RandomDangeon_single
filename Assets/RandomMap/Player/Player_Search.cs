using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Search : MonoBehaviour
{
    Vector3 focusPosition;
    Player_Move move;

    [SerializeField]
    MapManager mapCreater;
    [SerializeField]
    GameObject focus;

    public Vector3 GetFocusPosition()
    {
        return focusPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Player_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.GetMode() != Player_Move.MoveMode.Search)
        {
            focus.SetActive(false);
            return;
        }
        else
        {
            focus.SetActive(true);
        }

        focusPosition = transform.position + move.GetBeforeVelocity();
        focusPosition = new Vector3(Mathf.Floor(focusPosition.x) + 0.5f, Mathf.Floor(focusPosition.y) + 0.5f, Mathf.Floor(focusPosition.z));
        focus.transform.position = focusPosition;


        MapData mapData = mapCreater.GetMapData(new Vector2(focusPosition.x, focusPosition.y));

        if (mapData != null)
        {
            if (mapData.blockType == BlockType.Structure)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (mapData.structure != null)
                    {
                        mapData.structure.Select();
                    }
                }
            }

        }
    }
}

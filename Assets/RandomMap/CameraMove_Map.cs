using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_Map : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    MapManager mapCreater;
    [SerializeField]
    Vector2Int aspect;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(player.position.x, player.position.y, pos.z);
        Vector2 leftTop = mapCreater.GetLeftTop();
        Vector2 rightBottom = mapCreater.GetRightBottom();

        float halfSizeY = Camera.main.orthographicSize;
        float halfSizeX = halfSizeY * (16.0f / 9.0f);
        //âE
        if (pos.x + halfSizeX > rightBottom.x)
        {
            pos.x = rightBottom.x - halfSizeX;
        }
        //ç∂
        if (pos.x - halfSizeX < leftTop.x)
        {
            pos.x = leftTop.x + halfSizeX;
        }
        //è„
        if (pos.y + halfSizeY > leftTop.y)
        {
            pos.y = leftTop.y - halfSizeY;
        }
        //â∫
        if (pos.y - halfSizeY < rightBottom.y)
        {
            pos.y = rightBottom.y + halfSizeY;
        }

        transform.position = pos;
    }
}

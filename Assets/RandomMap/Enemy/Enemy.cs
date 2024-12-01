using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : Entity
{
    GameObject player;
    [SerializeField]
    float searchDistance;

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public bool IsPlayerInSerachRange()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 pos = transform.position;

        if (Vector3.Distance(pos, playerPos) < searchDistance)
        {
            return true;
        }
        return false;
    }
    public bool IsSightPlayer()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos, (playerPos - pos).normalized, Vector3.Distance(pos, playerPos));
        Debug.DrawRay(pos, (playerPos - pos).normalized * Vector3.Distance(pos, playerPos), Color.green);
        if (hit.collider.tag == "Player")
        {
            Debug.Log("player�����F���Ă��܂�");
            return true;
        }
        
        Debug.Log("player��������܂���Bhit : " + hit.collider.name);
        return false;
        
    }

    
}

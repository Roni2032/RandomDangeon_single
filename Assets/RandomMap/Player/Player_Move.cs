using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public enum MoveMode
    {
        Search,
        Battle
    }
    MoveMode moveMode;
    [SerializeField]
    float speed;
    Rigidbody2D rb;

    Vector3 velocity;
    Vector3 beforeVelocity;

    Player_Bag bag;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bag = GetComponent<Player_Bag>();
        moveMode = MoveMode.Search;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
    public Vector3 GetBeforeVelocity()
    {
        return beforeVelocity;
    }
    public MoveMode GetMode()
    {
        return moveMode;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!bag.IsOpen())
            {
                bag.Open();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (bag.IsOpen())
            {
                bag.Close();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (bag.IsOpen()) return;
        Vector3 pos = transform.position;

        velocity = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            velocity += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right;
        }
        pos += velocity.normalized * speed * Time.deltaTime;
        rb.MovePosition(pos);
        if (velocity.sqrMagnitude > 0)
        {
            beforeVelocity = velocity;
            float rad = Mathf.Atan2(-velocity.x, velocity.y);
            transform.eulerAngles = new Vector3(0, 0, rad * Mathf.Rad2Deg);
        }
    }
}

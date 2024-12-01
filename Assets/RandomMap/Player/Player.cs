using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    MapManager mapManager;

    Rigidbody2D rb;
    SpriteRenderer spriteRndr;

    Vector3 velocity;
    Vector3 beforeVelocity;

    Player_Bag bag;

    Vector2Int foucus;
    public Player_Bag GetBag()
    {
        return bag;
    }
    public Vector2Int GetFoucus()
    {
        return foucus;
    }
    public Vector3 GetVelocity()
    {
        return velocity;
    }
    public Vector3 GetBeforeVelocity()
    {
        return beforeVelocity;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRndr = GetComponent<SpriteRenderer>();
        bag = GetComponent<Player_Bag>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenBag();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBag();
        }
        Battle();
        FindMap();
    }
    void FixedUpdate()
    {
        if (!bag.IsOpen())
        {
            Move();
        }
    }
    private void Battle()
    {
        if(Input.GetMouseButtonDown(0))
        {

        }
    }
    private void FindMap()
    {
        Vector3 worldFoucusPosition = transform.position + beforeVelocity;
        foucus = mapManager.GetMapIndex(new Vector2(worldFoucusPosition.x, worldFoucusPosition.y));

        //foucusObject.transform.position = new Vector3(Mathf.Floor(worldFoucusPosition.x) + 0.5f, Mathf.Floor(worldFoucusPosition.y) + 0.5f, Mathf.Floor(worldFoucusPosition.z));


        MapData mapData = mapManager.GetMapData(foucus);

        if (mapData != null)
        {
            if (mapData.blockType == BlockType.Structure)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (mapData.structure != null)
                    {
                        mapData.structure.Select(mapManager.GetMapData(), foucus,this);
                    }
                }
            }

        }
    }
    private void OpenBag()
    {
        if (!bag.IsOpen())
        {
            bag.Open();
        }
    }
    private void CloseBag()
    {
        if (bag.IsOpen())
        {
            bag.Close();
        }
    }
    private void Move()
    {
        Vector3 pos = transform.position;

        velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
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
        pos += velocity.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(pos);
        if (velocity.sqrMagnitude > 0)
        {
            beforeVelocity = velocity;
            //float rad = Mathf.Atan2(-velocity.x, velocity.y);
            //transform.eulerAngles = new Vector3(0, 0, rad * Mathf.Rad2Deg);
            if (velocity.x < 0)
            {
                spriteRndr.flipX = true;
            }
            else if (velocity.x > 0)
            {
                spriteRndr.flipX = false;
            }
        }
    }
}

using UnityEngine;

public class Slime : Enemy
{
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerInSerachRange() && IsSightPlayer())
        {

        }
    }
}

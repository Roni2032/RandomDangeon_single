using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Battle : Entity_Battle
{

    Skill skill;

    Player_Move move;
    void Start()
    {
        move = GetComponent<Player_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.GetMode() != Player_Move.MoveMode.Battle) return;
    }
}

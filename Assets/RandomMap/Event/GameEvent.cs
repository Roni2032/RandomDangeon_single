using System;
using UnityEngine;

public class GameEvent<T>
{
    static Action<T> events;
    public GameEvent()
    {
        
    }
    public static void AddListener(Action<T> action)
    {
        events += action;
    }
    public static void DeleteListener(Action<T> action)
    {
        events -= action;
    }
    public static void Invok(T e)
    {
        events.Invoke(e);
    }
}
public class GetStructureItemEvent : GameEvent<GetStructureItemEvent>
{
    public GameObject player;
    public Structure structure;
    public Item items;
    public string item;

    public GetStructureItemEvent( GameObject player, Structure structure, string item)
    {
        this.player = player;
        this.structure = structure;
        this.item = item;
    }
}

public class AttackEvent : GameEvent<AttackEvent>
{
    public GameObject attacker;
    public GameObject target;
    public float damage;
    
    public AttackEvent( GameObject attacker, GameObject target, float damage)
    {
        this.attacker = attacker;
        this.target = target;
        this.damage = damage;
    }
}

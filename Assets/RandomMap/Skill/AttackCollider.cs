using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public enum ColliderType
    {
        Box,
        Cicle
    }
    public enum Effect
    {

    }

    public ColliderType colliderType;

    List<Effect> effects;
    
    float colliderRadius;
    Vector2 colliderSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

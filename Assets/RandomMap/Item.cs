using UnityEngine;

public class Item
{
    public string id;
    const int MAX_STACK = 32;
    int stack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int GetStack()
    {
        return stack;
    }
}

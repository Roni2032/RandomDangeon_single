using UnityEngine;
using UnityEngine.UI;

public class Player_Bag : MonoBehaviour
{
    [SerializeField]
    GameObject bagImage;
    [SerializeField]
    GameObject simpleBagImage;

    Item[] items;
    Vector2Int itemSlotSize;

    int selectIndex;
    bool isOpen = false;
    void Start()
    {
        itemSlotSize = new Vector2Int(9, 3);
        items = new Item[itemSlotSize.x * itemSlotSize.y];
    }
    public void Open() { 
        isOpen = true;
        bagImage.SetActive(true);
        simpleBagImage.SetActive(false);
    }
    public void Close() { 
        isOpen = false;
        bagImage.SetActive(false);
        simpleBagImage.SetActive(true);
    }
    public bool IsOpen()
    {
        return isOpen;
    }
    public void AddItem(Item item) { }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            string key = Input.inputString;
            if(key != "")
            {
                int.TryParse(key, out selectIndex);
                selectIndex--;

                if (selectIndex < 0)
                {
                    selectIndex = 0;
                }
                if (selectIndex >= itemSlotSize.x)
                {
                    selectIndex = itemSlotSize.x;
                }
            }
            
        }
    }
}

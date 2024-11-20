using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    Player_Bag bag;
    int lookIndex;
    Sprite itemSprite;

    Image slotImage;

    public void SetLookUpIndex(int index)
    {
        lookIndex = index;
    }
    void SetItem(Item item)
    {
        
    }
    void Start()
    {
        slotImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Item item = bag.GetItem(lookIndex);
        //スプライト表示
        //slotImage.sprite = item.
    }
}

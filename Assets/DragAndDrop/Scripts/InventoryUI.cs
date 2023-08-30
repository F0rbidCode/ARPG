using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public ShopItemUI itemPrefab;
    public Slot slotPrefab;

    public float rotate;

    Slot[] slots;
   
    // Start is called before the first frame update
    void Start()
    {
        // create a slot for each item
        slots = new Slot[inventory.items.Length];
        for (int i = 0; i < inventory.items.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform);
            //create a shop item UI and feed the item to it
            slots[i].itemUI = Instantiate(itemPrefab, slots[i].transform);
            slots[i].itemUI.SetItem(inventory.items[i]);
            //create a slot
            slots[i].Init(this, i, slots[i].itemUI);

            slots[i].transform.Rotate(0, 0, rotate);
        }    

    }
}

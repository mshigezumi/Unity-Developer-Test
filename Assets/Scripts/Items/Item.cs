using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    //private bool inInventory = false; //can probably just use -1 on inventorySlot to show it's not in an inventory
    private int inventorySlot = -1;

    public virtual void Use()
    {
        //Use the item
        //Something might happen

        Debug.Log("Using " + name);
    }

    //public void SetInInventory(bool input)
    //{
    //    inInventory = input;
    //}

    //public bool GetInInventory()
    //{
    //    return inInventory;
    //}

    public void SetInventorySlot(int input)
    {
        inventorySlot = input;
    }

    public int GetInventorySlot()
    {
        return inventorySlot;
    }
}

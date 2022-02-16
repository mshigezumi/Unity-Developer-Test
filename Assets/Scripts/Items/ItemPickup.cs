using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //this script attaches to the in-game item, allows it to be transfered into an item in an inventory, repurposed as random item generator
    private Item item;
    public List<Item> items = new List<Item>();

    public void Interact()
    {
        //Should be in a class that is inherited from as an override
        //base.Interact();

        Debug.Log("Interacting with " + item.name);

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        Inventory.instance.Add(item);
    }

    public void SelectRandomItem()
    {
        int rand = Random.Range(0, items.Count);
        item = Object.Instantiate(items[rand]);

        PickUp();
    }

}

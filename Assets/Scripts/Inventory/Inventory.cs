using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public InventoryUI inventoryUI;
    public TMP_Text itemName;

    public int space = 18;

    public List<Item> items = new List<Item>();
    public bool[] itemInSlot = new bool[18];

    public Item selectedItem = null;
    public int selectedItemIndex = -1;
    public int cursorIndex = 0;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }

        int index = GetRandomOpenSlot();
        Debug.Log("Adding " + item.name + " to slot " + index);

        itemInSlot[index] = true;
        //item.SetInInventory(true);
        item.SetInventorySlot(index);
        items.Add(item);

        //Debug.Log(GetInventorySlotsOfItems());

        SortItems();

        //Debug.Log(GetInventorySlotsOfItems());

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        itemInSlot[item.GetInventorySlot()] = false;
        //item.SetInInventory(false);
        item.SetInventorySlot(-1);
        items.Remove(item);

        DeselectItem();

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SortItems()
    {
        items.Sort((x, y) => x.GetInventorySlot().CompareTo(y.GetInventorySlot()));
    }

    public int GetRandomOpenSlot()
    {
        List<int> openSlots = new List<int>();
        for (int i = 0; i < itemInSlot.Length; i++)
        {
            if (!itemInSlot[i])
            {
                openSlots.Add(i);
            }
        }
        int rand = Random.Range(0, openSlots.Count);
        return openSlots[rand];
    }

    public void Clear()
    {
        items.Clear();
        for (int i = 0; i < itemInSlot.Length; i++)
        {
            itemInSlot[i] = false;
        }

        DeselectItem();

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }    

    public void RefreshItemInSlot()
    {
        for (int i = 0; i < itemInSlot.Length; i++)
        {
            itemInSlot[i] = false;
        }
        for (int i = 0; i < items.Count; i++)
        {
            itemInSlot[items[i].GetInventorySlot()] = true;
        }
    }

    public void MoveCursorTo(int index)
    {
        cursorIndex = index;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool SelectItem(Item item)
    {
        selectedItem = item;
        selectedItemIndex = item.GetInventorySlot();

        Debug.Log("Selecting " + selectedItem.name + " at index " + selectedItemIndex);

        inventoryUI.getSlots()[selectedItemIndex].icon.color = new Color(1f, 1f, 1f, 0.5f);

        itemName.text = item.name;
        itemName.enabled = true;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void DeselectItem()
    {
        if (selectedItem != null)
        {
            inventoryUI.getSlots()[selectedItemIndex].icon.color = new Color(1f, 1f, 1f, 1f);
        }
        itemName.enabled = false;
        selectedItem = null;
        selectedItemIndex = -1;
    }

    public bool SwapItems()
    {
        Item swapItem = null;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetInventorySlot() == cursorIndex)
            {
                swapItem = items[i];
            }
        }

        selectedItem.SetInventorySlot(cursorIndex);
        swapItem.SetInventorySlot(selectedItemIndex);

        Debug.Log("Swapping index " + selectedItemIndex + " and " + cursorIndex);

        DeselectItem();
        RefreshItemInSlot();
        SortItems();

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public bool MoveItem()
    {
        selectedItem.SetInventorySlot(cursorIndex);

        Debug.Log("Moving index " + selectedItemIndex + " to " + cursorIndex);

        DeselectItem();
        RefreshItemInSlot();
        SortItems();

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public string GetInventorySlotsOfItems() //only used for debugging
    {
        int[] array = new int[items.Count];
        int index = 0;

        foreach (var item in items)
        {
            array[index] = item.GetInventorySlot();
            index++;
        }

        string result = array.Aggregate(string.Empty, (s, i) => s + i.ToString() + " ");

        return result;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    PlayerControls controls;

    //Vector2 move;

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    public ItemPickup itemPickup;
    public AudioManager audioManager;

    int resolutionIndex;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Inventory.ShowHideInventory.performed += ctx => Show();
        //controls.Inventory.MoveCursor.performed += ctx => move = ctx.ReadValue<Vector2>(); //do I need to change this to individual inputs to not use update?
        //controls.Inventory.MoveCursor.canceled += ctx => move = Vector2.zero;
        controls.Inventory.MoveCursorUp.performed += ctx => CursorUp();
        controls.Inventory.MoveCursorDown.performed += ctx => CursorDown();
        controls.Inventory.MoveCursorLeft.performed += ctx => CursorLeft();
        controls.Inventory.MoveCursorRight.performed += ctx => CursorRight();
        controls.Inventory.Select.performed += ctx => Select();
        controls.Inventory.RemoveReset.performed += ctx => Remove();
        controls.Inventory.ChangeResolution.performed += ctx => ChangeResolution(ctx.ReadValue<float>());

        Screen.SetResolution(1920, 1080, false);
        resolutionIndex = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        ResetInventory();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        //transform.Translate(m, Space.World);

        //if (move.x < 0) //Left
        //{
        //    CursorLeft();
        //}
        //else if (move.x > 0) //Right
        //{
        //    CursorRight();
        //}

        //if (move.x < 0) //Down
        //{
        //    CursorDown();
        //}
        //else if (move.x > 0) //Up
        //{
        //    CursorUp();
        //}
    }

    public InventorySlot[] getSlots()
    {
        return slots;
    }

    void Show()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf); //need to test if controls are still active while hidden... ONLY show should work, not required for test
        //controls.Inventory.Select.Disable();
    }

    void CursorLeft()
    {
        if (inventory.cursorIndex > 0 && inventory.cursorIndex != 6 && inventory.cursorIndex != 12)
        {
            inventory.MoveCursorTo(inventory.cursorIndex - 1);
            audioManager.Play("MoveCursor");
        }
        else
        {
            audioManager.Play("Error");
        }
    }

    void CursorRight()
    {
        if (inventory.cursorIndex < inventory.space - 1 && inventory.cursorIndex != 5 && inventory.cursorIndex != 11)
        {
            inventory.MoveCursorTo(inventory.cursorIndex + 1);
            audioManager.Play("MoveCursor");
        }
        else
        {
            audioManager.Play("Error");
        }
    }

    void CursorDown()
    {
        if (inventory.cursorIndex < inventory.space - 6) //0-11 can go down
        {
            inventory.MoveCursorTo(inventory.cursorIndex + 6);
            audioManager.Play("MoveCursor");
        }
        else
        {
            audioManager.Play("Error");
        }
    }

    void CursorUp()
    {
        if (inventory.cursorIndex > 5) //6-17 can go up
        {
            inventory.MoveCursorTo(inventory.cursorIndex - 6);
            audioManager.Play("MoveCursor");
        }
        else
        {
            audioManager.Play("Error");
        }
    }

    void Select()
    {
        if (inventory.selectedItem == null)
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.cursorIndex == inventory.items[i].GetInventorySlot())
                {
                    inventory.SelectItem(inventory.items[i]);
                    audioManager.Play("Select");
                }
            }
        }
        else if (inventory.selectedItem != null)
        {
            if (inventory.cursorIndex != inventory.selectedItemIndex && inventory.itemInSlot[inventory.cursorIndex])
            {
                inventory.SwapItems();
                audioManager.Play("Place");
            }
            else// if (inventory.cursorIndex != inventory.selectedItemIndex && !inventory.itemInSlot[inventory.cursorIndex])
            {
                inventory.MoveItem();
                audioManager.Play("Place");
            }
        }
    }

    void Remove()
    {
        if (inventory.selectedItem != null)
        {
            inventory.Remove(inventory.selectedItem);
            audioManager.Play("Delete");
        }
        else
            ResetInventory();
    }

    void ResetInventory()
    {
        if (inventory.selectedItem == null)
        {
            inventory.Clear();

            for (int i = 0; i < 5; i++)
            {
                itemPickup.SelectRandomItem();
            }
            audioManager.Play("SpawnItems");
        }
    }
    void ChangeResolution(float input)
    {
        int[] widths = new int[] { 1280, 1920, 3860 }; //I think there was a typo in the instructions, width should be 3840 for 4K standard
        int[] heights = new int[] { 720, 1080, 1260 };

        if (input == 1) //right
        {
            if (resolutionIndex == 2)
                resolutionIndex = 0;
            else
                resolutionIndex++;

            Screen.SetResolution(widths[resolutionIndex], heights[resolutionIndex], false);
            Debug.Log("Changing resolution to " + widths[resolutionIndex] + "x" + heights[resolutionIndex]);
        }
        else if (input == -1) //left
        {
            if (resolutionIndex == 0)
                resolutionIndex = 2;
            else
                resolutionIndex--;

            Screen.SetResolution(widths[resolutionIndex], heights[resolutionIndex], false);
            Debug.Log("Changing resolution to " + widths[resolutionIndex] + "x" + heights[resolutionIndex]);
        }
        audioManager.Play("ResolutionSwitch");
    }

    private void OnEnable()
    {
        controls.Inventory.Enable();
    }

    private void OnDisable()
    {
        controls.Inventory.Disable();
    }

    void UpdateUI()
    {
        //List<Item> sorted = inventory.items.OrderBy(x => x.GetInventorySlot()).ToList();
        int index = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (inventory.itemInSlot[i])
            {
                slots[i].AddItem(inventory.items[index]); //sorted[index]
                index++;
            }
            else
            {
                slots[i].ClearSlot();
            }

            if (inventory.cursorIndex == i)
            {
                slots[i].ShowCursor();
            }
            else
            {
                slots[i].HideCursor();
            }
        }
    }
}

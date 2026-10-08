using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    public ItemSlot[] itemSlot;
    InputAction inventoryAction;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;
    private bool InventoryActive;

    void Start()
    {
        InventoryCanvas.SetActive(false);
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        InventoryActive = false;

        CreateSlots();
    }

    private void CreateSlots()
    {
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");

        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found in a Resources folder.", this);
            itemSlot = new ItemSlot[0];
            return;
        }

        Transform parent = InventoryCanvas.transform;

        GridLayoutGroup grid = InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);

        if (grid != null)
        {
            parent = grid.transform;
        }

        itemSlot = new ItemSlot[slotCount];

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, parent);
        }
    }

    void Update()
    {
        if (inventoryAction.triggered && InventoryCanvas != null && InventoryActive == false)
        {
            InventoryActive = true;
            Time.timeScale = 0f;
            Debug.Log("Inventory is active");
            InventoryCanvas.SetActive(true);
        }
        else if (inventoryAction.triggered && InventoryCanvas != null && InventoryActive == true)
        {
            InventoryActive = false;
            Time.timeScale = 1f;
            Debug.Log("Inventory is deactivated");
            InventoryCanvas.SetActive(false);
        }
    }

    public void AddItem(ItemDetails item)
    {
        Debug.Log("Item added to inventory: " + item.ItemName + " Quantity: " + item.Quantity);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDetails", menuName = "Scriptable Objects/ItemDetails")]
public class ItemDetails : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string itemName = "New Item";
    [SerializeField] private int quantity = 1;
    [SerializeField] private int maxStack = 64;
    [SerializeField] private Sprite itemIcon;

    [Header("Description")]
    [TextArea(3, 5)]
    [SerializeField] private string itemDescription;

    public string ItemName => itemName;
    public int Quantity => quantity;
    public int MaxStack => maxStack;
    public Sprite ItemIcon => itemIcon;
    public string ItemDescription => itemDescription;
}
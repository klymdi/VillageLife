using UnityEngine;

public class LumberStorage : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectHighlight highlight;
    [SerializeField] private StorageGridBuilder gridBuilder;

    private IPickable[] storedItems;

    public BuildingMaterialsSO Data => null;
    public Transform Transform => transform;

    private void Awake()
    {
        if (gridBuilder != null)
        {
            storedItems = new IPickable[gridBuilder.TotalSlots];
        }
    }

    public string GetInteractionText()
    {
        int count = GetCurrentCount();
        if (count >= gridBuilder.TotalSlots) return "Storage is full";
        return $"Store/Take Item ({count}/{gridBuilder.TotalSlots})";
    }

    public void Interact(PlayerController player)
    {
        if (player.IsHoldingItem)
        {
            int freeIndex = System.Array.IndexOf(storedItems, null);

            if (freeIndex != -1)
            {
                IPickable itemToStore = player.GetTopItem();
                StoreItem(itemToStore, freeIndex);
                player.RemoveTopItem();
            }
        }
        else
        {
            int lastIndex = GetLastOccupiedIndex();

            if (lastIndex != -1)
            {
                IPickable itemToTake = storedItems[lastIndex];
                storedItems[lastIndex] = null;
                player.pickUpObject(PlayerController.PickupPlace.Shoulder, itemToTake);
            }
        }
    }

    private void StoreItem(IPickable item, int index)
    {
        storedItems[index] = item; 

        Transform itemT = item.Transform;
        itemT.SetParent(gridBuilder.transform);

        itemT.localPosition = gridBuilder.GetSlotLocalPosition(index);
        itemT.localRotation = gridBuilder.GetItemRotation();
        itemT.localScale = Vector3.one;

        if (itemT.TryGetComponent<Rigidbody>(out Rigidbody rb))
            rb.isKinematic = true;

        if (itemT.TryGetComponent<Collider>(out Collider col))
            col.enabled = true;

        Debug.Log($"Stored at slot {index}. Position: {itemT.localPosition}");
    }

    private int GetLastOccupiedIndex()
    {
        for (int i = storedItems.Length - 1; i >= 0; i--)
        {
            if (storedItems[i] != null) return i;
        }
        return -1;
    }

    private int GetCurrentCount()
    {
        int count = 0;
        foreach (var item in storedItems)
        {
            if (item != null) count++;
        }
        return count;
    }

    public void OnFocus() => highlight?.HighlightObject();
    public void OnBlur() => highlight?.ResetHighlight();
}
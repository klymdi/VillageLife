using UnityEngine;

public class Plank : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectHighlight highlight;
    [SerializeField] private BuildingMaterialsSO buildingMaterialsSO;

    public BuildingMaterialsSO Data => buildingMaterialsSO;
    public Transform Transform => transform;

    public string GetInteractionText()
    {
        return "Press LMB to take Plank";
    }

    public void OnFocus() => highlight?.HighlightObject();
    public void OnBlur() => highlight?.ResetHighlight();

    public void Interact(PlayerController player)
    {
    }

    public void Pickup(PlayerController player)
    {
        player.pickUpObject(PlayerController.PickupPlace.Shoulder, this);
    }

    public void Drop(PlayerController player)
    {
        player.dropObject();
    }
}

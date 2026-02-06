using UnityEngine;

public class Plank : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ObjectHighlight highlight;

    public string GetInteractionText()
    {
        return "Press E to take Plank";
    }

    public void OnFocus() => highlight?.HighlightObject();
    public void OnBlur() => highlight?.ResetHighlight();

    public void Interact(PlayerController player)
    {
        player.pickUpObject("Hand", transform);
    }

    public void Pickup(PlayerController player)
    {
    }
}

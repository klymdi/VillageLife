using UnityEngine;

public class Plank : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "Press E to take Plank";
    }

    public void Interact(PlayerController player)
    {
        Debug.Log("plankTook");
    }
}

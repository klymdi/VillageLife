using UnityEngine;

public interface IInteractable
{
    string GetInteractionText();
    void Interact(PlayerController player);

    void OnFocus(); 
    void OnBlur(); 
}

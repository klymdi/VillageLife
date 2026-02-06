using UnityEngine;

public interface IInteractable: IPickable
{
    string GetInteractionText();
    void Interact(PlayerController player);

    void OnFocus(); 
    void OnBlur(); 
}

using UnityEngine;
using UnityEngine.UI;

public class InteractiveCrosshair : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 5f;

    [Header("References")]
    [SerializeField] private Image crosshairImage;
    [SerializeField] private PlayerController playerController; 

    private IInteractable currentInteractable;

    void Update()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null) currentInteractable.OnBlur();

                    currentInteractable = interactable;
                    currentInteractable.OnFocus();

                    if (InteractionPanelUI.Instance != null)
                        InteractionPanelUI.Instance.ShowPanel(currentInteractable.GetInteractionText());
                }

                if (Input.GetMouseButtonDown(0))
                {
                    currentInteractable.Pickup(playerController);
                }

                return; 
            }
        }

        ClearInteraction();
    }

    private void ClearInteraction()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnBlur();
            currentInteractable = null;

            if (InteractionPanelUI.Instance != null)
                InteractionPanelUI.Instance.HidePanel();
        }
    }
}
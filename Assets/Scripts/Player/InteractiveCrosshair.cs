using UnityEngine;
using UnityEngine.UI;

public class InteractiveCrosshair : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;
    [SerializeField] private float interactionDistance = 10f;

    private IInteractable currentInteractable;
    private Outline lastOutline;

    void Update()
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
                    currentInteractable = interactable;

                    if (InteractionPanelUI.Instance != null)
                        InteractionPanelUI.Instance.ShowPanel(interactable.GetInteractionText());
                }

                HandleOutline(hit.transform);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // —юди треба передати посиланн€ на плеЇра
                    // interactable.Interact(thisPlayerReference);
                }
            }
            else
            {
                ClearInteraction();
            }
        }
        else
        {
            ClearInteraction();
        }
    }

    private void HandleOutline(Transform hitTransform)
    {
        if (lastOutline == null || hitTransform != lastOutline.transform)
        {
            if (lastOutline != null) lastOutline.enabled = false;

            if (hitTransform.TryGetComponent<Outline>(out Outline newOutline))
            {
                lastOutline = newOutline;
                lastOutline.enabled = true;
            }
        }
    }

    private void ClearInteraction()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            if (InteractionPanelUI.Instance != null)
                InteractionPanelUI.Instance.HidePanel();
        }

        if (lastOutline != null)
        {
            lastOutline.enabled = false;
            lastOutline = null;
        }
    }
}
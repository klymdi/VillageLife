using UnityEngine;

public class LumberStorage : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectHighlight highlight;
    [SerializeField] private Transform placementGrid;
    public string GetInteractionText()
    {
        return "Press E to Interact";
    }

    public void Interact(PlayerController player)
    {
        
    }

    public void OnFocus() => highlight?.HighlightObject();
    public void OnBlur() => highlight?.ResetHighlight();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    [SerializeField] private GameObject objectBasicVisual;
    [SerializeField] private GameObject objectInteractionVisual;

    public void HighlightObject()
    {
        objectBasicVisual.SetActive(false);
        objectInteractionVisual.SetActive(true);
    }
    public void ResetHighlight()
    {
        objectBasicVisual.SetActive(true);
        objectInteractionVisual.SetActive(false);
    }
}
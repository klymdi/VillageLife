using UnityEngine;
using UnityEngine.UI;

public class InteractiveCrosshair : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;
    [SerializeField] private float interactionDistance = 10f;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color detectColor = Color.red;

    private Outline lastOutline;

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (lastOutline == null || hit.transform != lastOutline.transform)
            {
                if (lastOutline != null) lastOutline.enabled = false;

                if (hit.transform.TryGetComponent<Outline>(out Outline newOutline))
                {
                    lastOutline = newOutline;
                    lastOutline.enabled = true;
                }
            }
        }
        else if (lastOutline != null)
        {
            lastOutline.enabled = false;
            lastOutline = null;
        }
    }
}

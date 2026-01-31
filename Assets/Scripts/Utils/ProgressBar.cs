using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    void Start()
    {
        fillImage.fillAmount = 1f;
    }

    public void SetValue(float value) {
        fillImage.fillAmount = value;
    }
}

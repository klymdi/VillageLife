using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [SerializeField] private PlayerNeedsSO playerNeed;
    private ProgressBar progressBar;


    private float currentValue;
    private float drainValue;
    private float maxValue;


    void Start()
    {
        progressBar = this.gameObject.GetComponent<ProgressBar>();

        drainValue = playerNeed.needDrain;
        maxValue = playerNeed.maxValue;
        currentValue = maxValue;
    }

    void Update()
    {
        currentValue -= drainValue * Time.deltaTime;
        progressBar.SetValue(currentValue / maxValue);
    }
}

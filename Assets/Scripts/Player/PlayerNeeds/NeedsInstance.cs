using UnityEngine;

[System.Serializable]
public class NeedsInstance
{
    public NeedsData data; 
    public float currentValue;

    public void Initialize()
    {
        currentValue = data.maxValue;
    }

    public void UpdateStat(float deltaTime)
    {
        currentValue -= data.drainRate * deltaTime;
        currentValue = UnityEngine.Mathf.Clamp(currentValue, 0, data.maxValue);

    }

    public float GetProgress() => currentValue / data.maxValue;
}

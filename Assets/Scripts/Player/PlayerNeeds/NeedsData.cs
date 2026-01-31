using UnityEngine;

[CreateAssetMenu(fileName = "NeedsData", menuName = "Scriptable Objects/NeedsData")]
public class NeedsData : ScriptableObject
{
    public string statName;
    private void OnValidate()
    {
        // Присвоюємо змінній statName назву самого файлу
        if (statName != name)
        {
            statName = name;
        }
    }
    public float drainRate; 
    public float maxValue = 100f;
}

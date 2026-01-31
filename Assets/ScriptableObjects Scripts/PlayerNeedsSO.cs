using UnityEngine;

[CreateAssetMenu(fileName = "PlayerNeedsSO", menuName = "Scriptable Objects/PlayerNeedsSO")]
public class PlayerNeedsSO : ScriptableObject
{
    public string needName;
    public float needDrain;
    public float maxValue;
}

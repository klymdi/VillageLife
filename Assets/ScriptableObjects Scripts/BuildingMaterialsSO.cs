using UnityEngine;


[CreateAssetMenu(fileName = "BuildingMaterialsSO", menuName = "Scriptable Objects/BuildingMaterialsSO")]
public class BuildingMaterialsSO : ScriptableObject
{
    public string materialName;
    public Vector3 pickupRotation; 
    public Sprite icon;
    public int stackUnitValue = 1;
    public int stackAmount = 6;


    public Quaternion GetPickupRotation() => Quaternion.Euler(pickupRotation);
}

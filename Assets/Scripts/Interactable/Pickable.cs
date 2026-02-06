using UnityEngine;

public interface IPickable
{
    BuildingMaterialsSO Data { get; }
    Transform Transform { get; }
    public void Pickup(PlayerController player);    
    public void Drop(PlayerController player);

}

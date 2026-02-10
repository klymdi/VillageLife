using UnityEngine;

public class StorageGridBuilder : MonoBehaviour
{
    public enum BuildOrder { WidthFirst, LengthFirst }

    [Header("Grid Dimensions")]
    public int columns = 2;    
    public int rows = 5;       
    public int maxHeight = 3;  

    [Header("Spacing")]
    public Vector3 spacing = new Vector3(0.6f, 0.2f, 1.2f); 

    [Header("Gizmo Settings")]
    public Vector3 boxSize = new Vector3(0.5f, 0.15f, 1.1f); 
    public Color gizmoColor = Color.yellow;

    [Header("Item Orientation")]
    public Vector3 itemLocalRotation;

    public Vector3 GetSlotLocalPosition(int index)
    {
        int itemsPerLayer = columns * rows;
        int level = index / itemsPerLayer; 
        int indexInLayer = index % itemsPerLayer; 

        int col = indexInLayer % columns;
        int row = indexInLayer / columns; 

        return new Vector3(
            col * spacing.x,
            level * spacing.y,
            row * spacing.z
        );
    }

    public Quaternion GetItemRotation() => Quaternion.Euler(itemLocalRotation);
    public int TotalSlots => columns * rows * maxHeight;

    private void OnDrawGizmos()
    {
        if (TotalSlots <= 0) return;

        Gizmos.color = gizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;

        for (int i = 0; i < TotalSlots; i++)
        {
            Vector3 pos = GetSlotLocalPosition(i);

            Matrix4x4 cubeMatrix = Matrix4x4.TRS(pos, GetItemRotation(), Vector3.one);
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix *= cubeMatrix;

            Gizmos.DrawWireCube(Vector3.zero, boxSize);

            Gizmos.matrix = oldMatrix;
            Gizmos.DrawSphere(pos, 0.03f);
        }
    }
}
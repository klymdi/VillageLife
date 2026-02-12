using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float lookXLimit = 80f;


    [Header("Carry Settings")]
    [SerializeField] private int maxStackSize = 5;
    [SerializeField] private float stackOffset = 0.1f;
    [SerializeField] private Transform shoulderPosition;
    [SerializeField] private Transform handPosition;

    private List<IPickable> heldItemsStack = new List<IPickable>();

    private CharacterController characterController;
    private Vector3 velocity;
    private float rotationX = 0;
    private bool isGrounded;

    public enum PickupPlace {Hand, Shoulder };
    void Start()
    {
        // DIMA LOX
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(1)) dropObject();
    }

    private void HandleMovement() {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        characterController.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void pickUpObject(PickupPlace place, IPickable pickable)
    {
        if (heldItemsStack.Count >= maxStackSize) return;

        if (heldItemsStack.Count > 0)
        {
            if (heldItemsStack[0].Data.materialName != pickable.Data.materialName) return;
        }

        heldItemsStack.Add(pickable);

        Transform itemTransform = pickable.Transform;
        Transform targetParent = (place == PickupPlace.Shoulder) ? shoulderPosition : handPosition;

        itemTransform.SetParent(targetParent);

        float yOffset = (heldItemsStack.Count - 1) * stackOffset;
        itemTransform.localPosition = new Vector3(0, yOffset, 0);
        itemTransform.localRotation = pickable.Data.GetPickupRotation();

        ToggleItemPhysics(itemTransform, false);
    }

    public void dropObject()
    {
        if (heldItemsStack.Count == 0) return;

        int lastIndex = heldItemsStack.Count - 1;
        IPickable itemToDrop = heldItemsStack[lastIndex];

        itemToDrop.Transform.SetParent(null);
        ToggleItemPhysics(itemToDrop.Transform, true);

        if (itemToDrop.Transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(playerCamera.transform.forward * 3f, ForceMode.Impulse);
        }

        heldItemsStack.RemoveAt(lastIndex);
    }

    private void ToggleItemPhysics(Transform t, bool isEnabled)
    {
        if (t.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = !isEnabled;
        if (t.TryGetComponent<Collider>(out Collider col)) col.enabled = isEnabled;
    }

    public IPickable GetTopItem()
    {
        if (heldItemsStack.Count == 0) return null;
        return heldItemsStack[heldItemsStack.Count - 1];
    }

    public void RemoveTopItem()
    {
        if (heldItemsStack.Count > 0)
        {
            heldItemsStack.RemoveAt(heldItemsStack.Count - 1);
        }
    }

    public bool IsHoldingItem => heldItemsStack.Count > 0;

}


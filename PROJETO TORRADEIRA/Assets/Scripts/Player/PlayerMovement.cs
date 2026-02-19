using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;

    public Transform cameraTransform;
    public Rigidbody rb;

    float xRotation = 0f;
    bool isGrounded;


    private Inventario inventario;
    private ItemColetavel itemPerto;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inventario = GetComponent<Inventario>();
    }

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        transform.position += move * moveSpeed * Time.deltaTime;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDelta.x);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        if (itemPerto != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            inventario.AdicionarItem(itemPerto.itemNome);
            Destroy(itemPerto.gameObject);
            itemPerto = null;
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        ItemColetavel item = other.GetComponentInParent<ItemColetavel>();

        if (item != null)
        {
            itemPerto = item;
            Debug.Log("Aperte E para coletar " + item.itemNome);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemColetavel item = other.GetComponentInParent<ItemColetavel>();

        if (item != null && item == itemPerto)
        {
            itemPerto = null;
        }
    }
}

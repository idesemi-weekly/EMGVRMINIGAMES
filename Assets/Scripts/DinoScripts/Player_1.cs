using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Player1 : NetworkBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public InputActionReference jumpAction;

    public float gravity = 19.62f;
    public float jumpForce = 8f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        jumpAction.action.Disable();
    }

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        MovementServerRpc();
    }

    [ServerRpc]
    private void MovementServerRpc()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded && jumpAction.action.triggered)
        {
            direction = Vector3.up * jumpForce;
        }

        character.Move(direction * Time.deltaTime);

        UpdateClientRpc(direction);
    }

    [ClientRpc]
    private void UpdateClientRpc(Vector3 serverDirection)
    {
        // Update position on clients
        if (!IsServer)
        {
            character.Move(serverDirection * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            
        if (other.CompareTag("ObstacleCollision"))
        {
            GameManager.Instance.GameOver(1);
        }
    }
}

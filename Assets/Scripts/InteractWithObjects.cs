using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [SerializeField] GameObject Key;
    [SerializeField] DoorController Door; // referencia al componente DoorController en la puerta

    public bool interactionWithKey = false;
    public bool interactionWithDoor = false;

    bool playerGotKey = false;

    private void Update()
    {
        if (interactionWithKey && Input.GetKeyDown(KeyCode.E))
        {
            GotKeyMessage();
            playerGotKey = true;
            if (Key != null) Destroy(Key);
        }

        // Desbloquear la puerta si el jugador tiene la llave y está frente a la puerta
        if (interactionWithDoor && playerGotKey && Door != null && !Door.isUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            UnlockDoorMessage();
            Door.Unlock();
            playerGotKey = false; // la llave se consume
            return;
        }

        // Alternar entre abrir y cerrar la puerta si está desbloqueada y no está animando
        if (interactionWithDoor && Door != null && Door.isUnlocked && !Door.isAnimating && Input.GetKeyDown(KeyCode.E))
        {
            if (Door.isOpen)
            {
                CloseDoorMessage();
                Door.Close();
            }
            else
            {
                OpenDoorMessage();
                Door.Open();
            }
        }
    }

    void GotKeyMessage() { print("llave obtenida"); }
    void UnlockDoorMessage() { print("puerta desbloqueada"); }
    void OpenDoorMessage() { print("puerta abierta"); }
    void CloseDoorMessage() { print("puerta cerrada"); }

    private void OnTriggerEnter(Collider interactCollider)
    {
        if (interactCollider.CompareTag("Key"))
            interactionWithKey = true;

        if (interactCollider.CompareTag("LockedDoor"))
            interactionWithDoor = true;
    }

    private void OnTriggerExit(Collider interactCollider)
    {
        if (interactCollider.CompareTag("Key"))
            interactionWithKey = false;

        if (interactCollider.CompareTag("LockedDoor"))
            interactionWithDoor = false;
    }
}
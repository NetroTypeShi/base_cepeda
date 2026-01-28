using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [SerializeField] GameObject Key;
    [SerializeField] GameObject Door; // Referencia a la puerta (puede ser normal o bloqueada)

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

        if (interactionWithDoor && Input.GetKeyDown(KeyCode.E))
        {
            // Verificar si la puerta tiene el componente DoorController (puerta bloqueada)
            DoorController lockedDoor = Door.GetComponent<DoorController>();
            if (lockedDoor != null)
            {
                HandleLockedDoor(lockedDoor);
                return;
            }

            // Verificar si la puerta tiene el componente NormalDoorBehavior (puerta normal)
            NormalDoorBehavior normalDoor = Door.GetComponent<NormalDoorBehavior>();
            if (normalDoor != null)
            {
                normalDoor.Toggle(); // Alternar entre abrir y cerrar
                return;
            }
        }
    }

    private void HandleLockedDoor(DoorController lockedDoor)
    {
        // Desbloquear la puerta si el jugador tiene la llave
        if (playerGotKey && !lockedDoor.isUnlocked)
        {
            UnlockDoorMessage();
            lockedDoor.Unlock();
            playerGotKey = false; // La llave se consume
            return;
        }

        // Alternar entre abrir y cerrar la puerta si está desbloqueada y no está animando
        if (lockedDoor.isUnlocked && !lockedDoor.isAnimating)
        {
            if (lockedDoor.isOpen)
            {
                CloseDoorMessage();
                lockedDoor.Close();
            }
            else
            {
                OpenDoorMessage();
                lockedDoor.Open();
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

        if (interactCollider.CompareTag("LockedDoor") || interactCollider.CompareTag("Door"))
        {
            interactionWithDoor = true;
            Door = interactCollider.gameObject; // Actualizar la referencia a la puerta
        }
    }

    private void OnTriggerExit(Collider interactCollider)
    {
        if (interactCollider.CompareTag("Key"))
            interactionWithKey = false;

        if (interactCollider.CompareTag("LockedDoor") || interactCollider.CompareTag("Door"))
        {
            interactionWithDoor = false;
            Door = null; // Limpiar la referencia a la puerta
        }
    }
}
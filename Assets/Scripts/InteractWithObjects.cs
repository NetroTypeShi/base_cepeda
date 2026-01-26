using System.Collections;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [SerializeField] GameObject Key;
    [SerializeField] GameObject Door;
    Vector3 doorStartPos;
    Vector3 doorEndPos;
    Quaternion doorStartRotation;
    Quaternion doorEndRotation;

    public bool interactionWithKey = false;
    public bool interactionWithDoor = false;

    bool playerGotKey = false;
    bool doorUnlocked = false; // Indica si la puerta está desbloqueada
    bool doorOpened = false;   // Indica si la puerta está abierta
    bool isAnimating = false;  // Nuevo: Indica si la puerta está en proceso de animación

    [SerializeField] float doorOpenAngle = 90f;
    [SerializeField] float doorSpeed = 2f;

    private void Update()
    {
        if (interactionWithKey && Input.GetKeyDown(KeyCode.E))
        {
            GotKeyMessage();
            playerGotKey = true;
            Destroy(Key);
        }

        // Desbloquear la puerta si el jugador tiene la llave y está frente a la puerta
        if (interactionWithDoor && playerGotKey && !doorUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            UnlockDoorMessage();
            doorUnlocked = true;
            playerGotKey = false; // Opcional: la llave se "consume"
        }
        // Alternar entre abrir y cerrar la puerta si está desbloqueada y no está animando
        else if (interactionWithDoor && doorUnlocked && !isAnimating && Input.GetKeyDown(KeyCode.E))
        {
            if (doorOpened)
            {
                CloseDoorMessage();
                StartCoroutine(ToggleDoor(false)); // Cerrar la puerta
            }
            else
            {
                OpenDoorMessage();
                StartCoroutine(ToggleDoor(true)); // Abrir la puerta
            }
        }
    }

    IEnumerator ToggleDoor(bool open)
    {
        isAnimating = true; // Bloquear interacción mientras la puerta se anima

        // Configurar los valores iniciales y finales para la posición y la rotación
        doorStartPos = Door.transform.position;
        doorStartRotation = Door.transform.rotation;

        if (open)
        {
            // Configurar la posición y rotación para el estado "abierto"
            doorEndPos = new Vector3(-18.715f, doorStartPos.y, 2.644f); // Cambia estos valores según lo necesites
            doorEndRotation = Quaternion.Euler(
                Door.transform.eulerAngles.x,
                Door.transform.eulerAngles.y + doorOpenAngle,
                Door.transform.eulerAngles.z
            );
        }
        else
        {
            // Configurar la posición y rotación para el estado "cerrado"
            doorEndPos = new Vector3(-18.06f, doorStartPos.y, 1.834f); // Cambia estos valores según lo necesites
            doorEndRotation = Quaternion.Euler(
                Door.transform.eulerAngles.x,
                Door.transform.eulerAngles.y - doorOpenAngle,
                Door.transform.eulerAngles.z
            );
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * doorSpeed;

            // Interpolación de posición
            Door.transform.position = Vector3.Lerp(doorStartPos, doorEndPos, t);

            // Interpolación de rotación
            Door.transform.rotation = Quaternion.Lerp(doorStartRotation, doorEndRotation, t);

            yield return null;
        }

        doorOpened = open; // Actualizar el estado de la puerta
        isAnimating = false; // Permitir interacción nuevamente
    }

    void GotKeyMessage()
    {
        print("llave obtenida");
    }

    void UnlockDoorMessage()
    {
        print("puerta desbloqueada");
    }

    void OpenDoorMessage()
    {
        print("puerta abierta");
    }

    void CloseDoorMessage()
    {
        print("puerta cerrada");
    }

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
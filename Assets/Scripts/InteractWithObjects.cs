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
    bool doorOpened = false;

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

        if (interactionWithDoor && playerGotKey && Input.GetKeyDown(KeyCode.E) && !doorOpened)
        {
            OpenDoorMessage();
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        doorOpened = true;

        // Configurar los valores iniciales y finales para la posición y la rotación
        doorStartPos = Door.transform.position;
        doorEndPos = new Vector3(-18.715f, doorStartPos.y, 2.644f); // Cambia estos valores según lo necesites

        doorStartRotation = Door.transform.rotation;
        doorEndRotation = Quaternion.Euler(
            Door.transform.eulerAngles.x,
            Door.transform.eulerAngles.y + doorOpenAngle,
            Door.transform.eulerAngles.z
        );

        float t = 0f;

        // Interpolar posición y rotación simultáneamente
        while (t < 1f)
        {
            t += Time.deltaTime * doorSpeed;

            // Interpolación de posición
            Door.transform.position = Vector3.Lerp(doorStartPos, doorEndPos, t);

            // Interpolación de rotación
            Door.transform.rotation = Quaternion.Lerp(doorStartRotation, doorEndRotation, t);

            yield return null;
        }
    }

    void GotKeyMessage()
    {
        print("llave obtenida");
    }

    void OpenDoorMessage()
    {
        print("puerta abierta");
    }

    private void OnTriggerEnter(Collider interactCollider)
    {
        if (interactCollider.CompareTag("InteractiveObject"))
            interactionWithKey = true;

        if (interactCollider.CompareTag("Door"))
            interactionWithDoor = true;
    }

    private void OnTriggerExit(Collider interactCollider)
    {
        if (interactCollider.CompareTag("InteractiveObject"))
            interactionWithKey = false;

        if (interactCollider.CompareTag("Door"))
            interactionWithDoor = false;
    }
}


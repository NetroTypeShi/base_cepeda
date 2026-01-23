using System.Collections;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [SerializeField] GameObject Key;
    [SerializeField] GameObject Door;

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

        Quaternion startRotation = Door.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(
            Door.transform.eulerAngles.x,
            Door.transform.eulerAngles.y + doorOpenAngle,
            Door.transform.eulerAngles.z
        );

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * doorSpeed;
            Door.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
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


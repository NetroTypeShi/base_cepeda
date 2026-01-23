using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [SerializeField] GameObject Key;
    [SerializeField] GameObject Door;
    [SerializeField] public bool interactionWithKey = false;
    [SerializeField] public bool interactionWithDoor = false;
    [SerializeField] bool playerGotKey = false;
    void Start()
    {

    }
    private void Update()
    {
        if (interactionWithKey == true && Input.GetKeyDown(KeyCode.E))
        {
            GotKeyMessage();
            playerGotKey = true;
            Destroy(Key);
        }
        if(interactionWithDoor == true && playerGotKey == true && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoorMessage();
            Destroy(Door);
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
        {
            interactionWithKey = true;
        }
        if (interactCollider.CompareTag("Door"))
        {
            interactionWithDoor = true;
        }

    }

    private void OnTriggerExit(Collider interactCollider)
    {
        if (interactCollider.CompareTag("InteractiveObject"))
        {
            interactionWithKey = false;
        }
        if (interactCollider.CompareTag("Door"))
        {
            interactionWithDoor = false;
        }
    }
}

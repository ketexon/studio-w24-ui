using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float sensitivity = 500;
    [SerializeField] float maxPitch = 80;
    [SerializeField] new Camera camera;
    [SerializeField] DialogueRunner dialogueRunner;

    CursorLockMode oldLockMode;
    bool paused = false;

    CharacterController controller;

    void Reset()
    {
        camera = GetComponentInChildren<Camera>();
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    void Start()
    {
    }

    void OnDialogueStart()
    {
        paused = true;
        oldLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnDialogueComplete()
    {
        Cursor.lockState = oldLockMode;
        paused = false;
    }

    void Update()
    {
        if(paused)
        {
            return;
        }

        var move = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
        ).normalized * speed * Time.deltaTime;

        var look = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            -Input.GetAxisRaw("Mouse Y")
        ) * sensitivity * Time.deltaTime;

        // yaw
        transform.Rotate(Vector3.up, look.x);
        move = transform.rotation * move;
        controller.Move(move);

        // pitch
        camera.transform.Rotate(Vector3.right, look.y);
        float cameraRot = camera.transform.localRotation.eulerAngles.x;
        cameraRot = (cameraRot + 180) % 360 - 180; // convert from [0,360) to [-180, 180) so we can clamp it easier

        // clamp pitch from minPitch to maxPitch
        camera.transform.localRotation = Quaternion.AngleAxis(
            Mathf.Clamp(cameraRot, -maxPitch, maxPitch),
            Vector3.right
        );

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

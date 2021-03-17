using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Flashlight flashlight = default;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 aimDir = cam.ScreenToWorldPoint(Input.mousePosition);
        flashlight.SetAimDirection(aimDir);
        flashlight.SetOrigin(transform.position);
    }
}

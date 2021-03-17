using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Flashlight flashlight = default;
    private Camera cam;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] float moveSpeed = default;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        SyncFlashlightWithPlayerMovement();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }

    void SyncFlashlightWithPlayerMovement()
    {
        Vector3 aimDir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        flashlight.SetAimDirection(aimDir);
        flashlight.SetOrigin(transform.position);
    }
}

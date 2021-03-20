﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Flashlight flashlight = default;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float fearMeter = 0;
    private GameObject target = default;
    [SerializeField] KeyCode interactKey = default;
    private bool isInteractingWithObject;
    private Animator anim;
    private bool isFacingRight;
    [SerializeField] Transform gfx;
    // up = 0, right = 1, down = 2, left = 3
    [SerializeField] BoxCollider2D[] interactionColliders;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.isPaused)
            return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        HandleMovementAnimation();

        flashlight.HandleAim(transform.position);

        if (Input.GetKeyDown(interactKey))
        {
            GetInteract();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }

    void GetInteract()
    {
        if (target == null)
            return;

        Interactable obj = target.GetComponent<Interactable>();
        if (obj != null)
        {
            obj.GetInteract();
            isInteractingWithObject = true;
        }
    }

    void HandleMovementAnimation()
    {
        if (movement != Vector2.zero)
        {
            ChangeInteractionCollider();
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        if (movement.x < 0 && isFacingRight || movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void ChangeInteractionCollider()
    {
        if (movement.y > 0.1f)
        {
            interactionColliders[0].enabled = true;
            interactionColliders[1].enabled = false;
            interactionColliders[2].enabled = false;
            interactionColliders[3].enabled = false;
        }
        else if (movement.y < -0.1f)
        {
            interactionColliders[0].enabled = false;
            interactionColliders[1].enabled = false;
            interactionColliders[2].enabled = true;
            interactionColliders[3].enabled = false;
        }

        if (movement.x > 0.1f)
        {
            interactionColliders[0].enabled = false;
            interactionColliders[1].enabled = true;
            interactionColliders[2].enabled = false;
            interactionColliders[3].enabled = false;
        }
        else if (movement.x < -0.1f)
        {
            interactionColliders[0].enabled = false;
            interactionColliders[1].enabled = false;
            interactionColliders[2].enabled = false;
            interactionColliders[3].enabled = true;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        gfx.Rotate(0, 180, 0);
    }

    public void GetDamage()
    {
        if (fearMeter > 0)
        {
            fearMeter -= Time.deltaTime;
            AnimationHandler.instance.GetDamage();

            if(fearMeter <= 0)
            {
                // game over
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        target = other.gameObject;

        if (other.tag == "Battery")
        {
            flashlight.batteryCount++;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            if (isInteractingWithObject)
                target.GetComponent<Interactable>().StopInteract();

            isInteractingWithObject = false;
            target = null;
        }
    }
}

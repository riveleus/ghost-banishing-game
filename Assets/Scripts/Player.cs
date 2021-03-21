using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Flashlight flashlight = default;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float sanity = 0;
    private Animator anim;
    private bool isFacingRight;
    private bool canMove = true;
    [SerializeField] Transform gfx;
    public bool haveSafetyMatches;
    private Candle currentCandle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        UIManager.instance.InitializeSanityBar();
    }

    void Update()
    {
        if (GameManager.instance.isStopped())
            return;

        if (canMove)
            MovementInput();

        HandleMovementAnimation();

        flashlight.HandleAim(transform.position);
    }

    void FixedUpdate()
    {
        if (canMove)
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }

    public void MovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void HandleMovementAnimation()
    {
        if (movement != Vector2.zero)
        {
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

    void Flip()
    {
        isFacingRight = !isFacingRight;
        gfx.Rotate(0, 180, 0);
    }

    public void GetDamage()
    {
        if (sanity > 0)
        {
            sanity -= Time.deltaTime * 10;
            float s = sanity / 100;
            UIManager.instance.UpdateSanityBar(s);
            AnimationHandler.instance.GetDamage();
            StopRollingCandle();

            if (sanity <= 0)
            {
                sanity = 0;
                GameManager.instance.GameOver();
            }
        }
    }

    public void RollCandle(Candle candle)
    {
        currentCandle = candle;
    }

    public void StopMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
        anim.SetBool("Walking", false);
    }

    public void AllowMovement()
    {
        canMove = true;
    }

    public void RemoveCurrentCandle()
    {
        currentCandle = null;
    }

    void StopRollingCandle()
    {
        if (currentCandle != null)
        {
            currentCandle.StopRolling();
            currentCandle = null;
            AllowMovement();
        }
    }
}

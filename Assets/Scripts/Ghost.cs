using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] Player player;
    private float moveSpeed;
    [SerializeField] float escapeSpeed;
    [SerializeField] float normalSpeed;
    private bool isFacingRight;
    private bool escaping;
    [SerializeField] Transform[] randomPositionBetweenPlayer;
    private bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (escaping)
        {
            movement = (transform.position - player.transform.position).normalized;
            moveSpeed = escapeSpeed;
        }
        else
        {
            movement = (player.transform.position - transform.position).normalized;
            moveSpeed = normalSpeed;
        }

        if (movement.x < 0 && isFacingRight || movement.x > 0 && !isFacingRight)
        {
            Flip();
        }

        if(isAttacking)
            player.GetDamage();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 5.3f)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
        else if(escaping)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }

    public void Escape(bool state)
    {
        escaping = state;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Disappear()
    {
        int index = Random.Range(0, randomPositionBetweenPlayer.Length);
        transform.position = randomPositionBetweenPlayer[index].position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.isTrigger)
        {
            isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && other.isTrigger)
        {
            isAttacking = false;
        }
    }
}

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

    public float healthBar = 10;
    public float heartbeatCounter = 10;
    public bool isBinded = false;
    public Transform flashlight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(healthBar>0 && !isBinded)
        {
            if (escaping)
            {
                if (!AudioManager.instance.sfx[1].isPlaying)
                {
                    AudioManager.instance.PlaySFX(1);
                }
                healthBar -= Time.deltaTime;
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

            if (isAttacking)
            {
                if(!AudioManager.instance.sfx[0].isPlaying)
                {
                    AudioManager.instance.PlaySFX(0);
                }
                
                player.GetDamage();
            }
            else if (!isAttacking && AudioManager.instance.sfx[0].isPlaying)
            {
                AudioManager.instance.sfx[0].Stop();
            }
        }
        else
        {
            if(healthBar >= 10)
            {
                isBinded = false;
                this.transform.SetParent(null);
                transform.localEulerAngles = new Vector3(0, 0, 0);
                Disappear();
            }
            else
            {
                isBinded = true;

                this.transform.SetParent(flashlight);
                // isFacingRight = !isFacingRight;
                transform.localPosition = new Vector3(6, 0, 0);
                // transform.localEulerAngles = new Vector3(0, 0, -flashlight.rotation.eulerAngles.z);
                healthBar += Time.deltaTime;
            }
        }

        
    }

    void FixedUpdate()
    {
        if (!isBinded)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > 5.3f)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
            }
            else if (escaping)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
            }
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
        if (!isBinded)
        {
            int index = Random.Range(0, randomPositionBetweenPlayer.Length);
            transform.position = randomPositionBetweenPlayer[index].position;
        }
        
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

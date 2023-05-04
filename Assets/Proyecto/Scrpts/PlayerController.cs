using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float speedInstance;
    public float fuerzaImpulso = 5;

    bool vulnerable;
    public int vida = 3;

    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private Animator animation;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        vulnerable = true;
        speedInstance = speed;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(horizontal * speed, fisica.velocity.y);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TocarSuelo())
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
        }

        if (fisica.velocity.x > 0) sprite.flipX = false;
        else if (fisica.velocity.x < 0) sprite.flipX = true;

        animarJugador();
    }

    private void animarJugador()
    {
        if (!TocarSuelo()) animation.Play("PlayerJumping");
        else if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerRunning");
        }
        else if ((fisica.velocity.x < 1 || fisica.velocity.x > -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerIdle");
        }
    }

    private bool TocarSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast
            (transform.position + new Vector3(0,-2f,0), Vector2.down, 0.2f);
        return toca.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            if (vulnerable == true)
            {
                vulnerable = false;
                vida--;
                Invoke("HacerVulnerable", 2f);
                sprite.color = Color.red;
            }        
            if (vida <= 0)
            {
            SceneManager.LoadScene("NVL-1");
            }
        }
    }

    void HacerVulnerable()
    {
        vulnerable = true;
        sprite.color = Color.white;
    }
}

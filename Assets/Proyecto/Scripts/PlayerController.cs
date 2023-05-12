using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float speedInstance;
    public float fuerzaImpulso = 5;
    float movimientoFloat;

    bool vulnerable;
    public int vida = 3;

    int puntuacion;

    private float tiempoInicio;
    public int tiempoNivel;
    public int tiempoEmpleado;

    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private Animator animation;

    public Canvas canvas;
    private ControlHUD hud;

    public static PlayerController instance;

    int haGanado;

    public AudioClip saltoSfx;
    public AudioClip vidaSfx;
    public AudioClip recolectarSfx;
    private AudioSource audiosource;

    bool isFalling = false;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        tiempoInicio = Time.time;
        vulnerable = true;
        speedInstance = speed;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        hud = canvas.GetComponent<ControlHUD>();
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //fisica.AddForce(Vector2.right * (horizontal * speed), ForceMode2D.Force);
        movimientoFloat = horizontal;
        fisica.velocity = new Vector2(horizontal * speed, fisica.velocity.y);
    }
    private void Update()
    {
        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && isFalling == false)
        {
            fisica.velocity = new Vector2(0f, 0f);
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
            audiosource.PlayOneShot(saltoSfx);
            isFalling = true;
        }

        //Volteo
        if (fisica.velocity.x > 0) sprite.flipX = false;
        else if (fisica.velocity.x < 0) sprite.flipX = true;

        animarJugador();

        hud.SetPuntuacion(GameObject.FindGameObjectsWithTag("PowerUp").Length);
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            GanarJuego();
        }

        tiempoEmpleado = (int)(Time.time - tiempoInicio);
        hud.SetTiempo(tiempoNivel - tiempoEmpleado);

        if (tiempoNivel - tiempoEmpleado < 0) FinJuego();

        if (fisica.velocity.y == 0)
        {
            isFalling = false;
        }
        else if (fisica.velocity.y != 0)
        {
            isFalling = true;
        }
    }

    private void animarJugador()
    {
        if (isFalling == true) animation.Play("PlayerJumping");
        else if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerRunning");
        }
        else if ((fisica.velocity.x < 1 || fisica.velocity.x > -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerIdle");
        }
    }

    /*private bool TocarSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast
            (transform.position + new Vector3(0,-2f,0), Vector2.down, 0.2f);
        return toca.collider != null;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            QuitarVida();
        }
    }

    public void QuitarVida()
    {
        if (vulnerable == true)
        {
            audiosource.PlayOneShot(vidaSfx);
            vulnerable = false;
            vida--;
            hud.SetVidas(vida);
            Invoke("HacerVulnerable", 1f);
            sprite.color = Color.red;
            if (vida == 0)
            {
                FinJuego();
            }
        }
    }

    void HacerVulnerable()
    {
        vulnerable = true;
        sprite.color = Color.white;
    }

    private void GanarJuego()
    {
        puntuacion += (vida * 100) + (tiempoNivel - tiempoEmpleado);
        PlayerPrefs.SetInt("HaGanado", 1);
        PlayerPrefs.SetInt("Puntuacion", puntuacion);
        Debug.Log("YOU WIN!!" + puntuacion);
        SceneManager.LoadScene("FinDePartida");
    }

    public void FinJuego()
    {
        PlayerPrefs.SetInt("HaGanado", 0);
        SceneManager.LoadScene("FinDePartida");
    }

    public void IncrementarPuntuacion(int cantidad)
    {
        puntuacion += cantidad;
        Debug.Log(puntuacion);
    }
}

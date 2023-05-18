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

    float horizontal;

    public AudioClip saltoSfx;
    public AudioClip vidaSfx;
    public AudioClip recolectarSfx;
    private AudioSource audiosource;

    bool isFalling = false;

    //Disparo:
    [SerializeField] GameObject disparoPrefab;
    public int lado;
    bool isShooting;
    bool isRunning;
    float timerDisparo = 0.5f;

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

        //fisica.AddForce(Vector2.right * (horizontal * speed), ForceMode2D.Force);
        movimientoFloat = horizontal;
        fisica.velocity = new Vector2(horizontal * speed, fisica.velocity.y);
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && isFalling == false)
        {
            fisica.velocity = new Vector2(0f, 0f);
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse);
            audiosource.PlayOneShot(saltoSfx);
            isFalling = true;
        }

        //Volteo
        if (fisica.velocity.x > 0)
        {
            sprite.flipX = false;
            lado = 1;
        }
        else if (fisica.velocity.x < 0)
        { 
            sprite.flipX = true;
            lado = -1;
        }

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

        //Disparo:
        if (Input.GetKeyUp(KeyCode.LeftShift) == true)
        {
            Disparo();
            isShooting = true;
        }
        if (isShooting == true)
        {
            timerDisparo -= Time.deltaTime;
            if (timerDisparo <= 0)
            {
                isShooting = false;
                timerDisparo = 0.5f;
            }
        }
    }

    private void animarJugador()
    {
        if (isFalling == true) animation.Play("PlayerJumping");
        else if (isShooting == false && (fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerRunning");
        }
        else if (isShooting == false && (fisica.velocity.x < 1 && fisica.velocity.x > -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerIdle");
        }
        else if(isShooting == true && (fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerShootingWalking");
        }
        else if(isShooting == true && (fisica.velocity.x < 1 && fisica.velocity.x > -1) && fisica.velocity.y == 0)
        {
            animation.Play("PlayerShootingIdle");
        }
    }

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

    //Disparo:
    void Disparo()
    {
        Instantiate(disparoPrefab, (Vector2)gameObject.transform.position + Vector2.right * lado, gameObject.transform.rotation);
    }
}

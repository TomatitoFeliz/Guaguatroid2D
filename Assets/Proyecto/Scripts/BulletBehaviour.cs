using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Animator animation;
    Rigidbody2D rigidbody2D;
    public float bulletSpeed = 25;
    public float animationImpactTimer = 0.3f;
    bool impact = false;
    
    private void Start()
    {
       
    }
    private void Awake()
    {
        animation = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(PlayerController.instance.lado * bulletSpeed,0), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "Paredes")
        {
            GetComponent<Collider2D>().enabled = false;
            animation.Play("ImpactBulletAnimation");
            impact = true;
        }
        if (collision.gameObject.tag == "Bala")
        {
            animation.Play("ImpactBulletAnimation");
            impact = true;
        }
    }
    private void Update()
    {
        if (impact == true)
        {
            animationImpactTimer -= Time.deltaTime;
        }
        if (animationImpactTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float bulletSpeed = 25;
    
    private void Start()
    {
       
    }
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(PlayerController.instance.lado * bulletSpeed,0), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mapa")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bala")
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

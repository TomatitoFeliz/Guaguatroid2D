using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int cantidad;
    public float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Slow(collision));
        }

        IEnumerator Slow(Collider2D Player)
        {
            PlayerController.instance.speed = 15;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(timer);

            PlayerController.instance.speed = PlayerController.instance.speedInstance;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    public float velocidad;
    public Vector3 posicionFin;

    private Vector3 posicionInicio;
    private bool movimientoFin;

    private SpriteRenderer sprite;

    private void Start()
    {
        posicionInicio = transform.position;
        movimientoFin = true;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoverEnemigo();
    }

    private  void MoverEnemigo()
    {
        Vector3 posicionDestino = (movimientoFin) ? posicionFin : 
            posicionInicio;

        transform.position = Vector3.MoveTowards(transform.position,
            posicionDestino, velocidad * Time.deltaTime);

        if (transform.position == posicionFin)
        {
            movimientoFin = false;
            sprite.flipX = true;
        }
        if (transform.position == posicionInicio)
        {
            movimientoFin = true; 
            sprite.flipX = false;
        }
    }
}

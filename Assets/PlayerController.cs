using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float fuerzaImpulso = 5;
    Rigidbody2D fisica;
    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
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
    }
    private bool TocarSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast
            (transform.position + new Vector3(0,-2f,0), Vector2.down, 0.2f);
        return toca.collider != null;
    }
}

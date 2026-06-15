using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{

    public float velocidad = 5f;
    public Animator animator;
    public GameObject balaPrefab;
    public float intervaloFuego = 0.5f;
    public float tiempoUltimoDisparo;
    private Rigidbody rb;
    float movX;
    float movZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Salto();
        Mover();

        if (Time.time >= tiempoUltimoDisparo + intervaloFuego)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Disparar();
            }
        }
    }


    void ReiniciarEscena()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            Invoke("ReiniciarEscena", 1f);
        }
    }


    void Mover()
    {
        movX = 0;
        movZ = 1; // constantemente moviendose hacia adelante

        if (Input.GetKey(KeyCode.A))
        {
            movX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movX = 1;
        }

        rb.velocity = new Vector3(movX * velocidad, rb.velocity.y, movZ * velocidad);
    }

    void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, 8f, rb.velocity.z);
        }
    }

    void Disparar()
    {
        tiempoUltimoDisparo = Time.time; 
        GameObject bala = Instantiate(balaPrefab, new Vector3(transform.position.x, 1f, transform.position.z) + transform.forward, Quaternion.identity);

        Rigidbody rbBala = bala.GetComponent<Rigidbody>();
        rbBala.velocity = transform.forward * 10;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{

    public float velocidadZ = 9f;
    public float velocidadX = 2.5f;
    public Animator animator;
    public GameObject balaPrefab;
    public float intervaloFuego = 1f;
    public float tiempoUltimoDisparo;
    public bool enSuelo;
    private Rigidbody rb;
    float movX;
    float movZ;
    float limiteLateral = 5.0f;
    public AudioClip sonidoDisparo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y <= 0.01f)
        {
            enSuelo = true;
        }
        else
        {
            enSuelo = false;
        }

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

        rb.velocity = new Vector3(movX * velocidadX, rb.velocity.y, movZ * velocidadZ);
        float posicionXFijada = Mathf.Clamp(transform.position.x, -limiteLateral, limiteLateral);

        if (transform.position.x != posicionXFijada)
        {
            transform.position = new Vector3(posicionXFijada, transform.position.y, transform.position.z);
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
    }

    void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5.6f, rb.velocity.z);
        }
    }

    void Disparar()
    {
        tiempoUltimoDisparo = Time.time; 
        GameObject bala = Instantiate(balaPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z) + transform.forward, Quaternion.identity);
        if (sonidoDisparo != null)
        {
        AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);
        }
        Rigidbody rbBala = bala.GetComponent<Rigidbody>();
        rbBala.velocity = transform.forward * 15;
    }
}

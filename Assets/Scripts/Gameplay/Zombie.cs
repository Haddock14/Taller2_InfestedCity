using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Zombie : MonoBehaviour
{
    // Velocidaddel zombie hacia la pantalla
    public float velocidadAvance = 5.0f;
    private GameManager GameManager;
    public AudioClip sonidoMuerte; 
    private AudioSource audioSource;
    void Start()
    {
        GameManager = Object.FindAnyObjectByType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        transform.Translate(-Vector3.back * velocidadAvance * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Bullet")) 
        {
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position); // Permite reproducir el sonido de muerte en la posición del zombie
            Destroy(gameObject); 
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if (GameManager != null)
            {
                GameManager.ReiniciarJuego();
            }
            else
            {
                Debug.LogError("GameManager no encontrado en la escena.");
            }
        }
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
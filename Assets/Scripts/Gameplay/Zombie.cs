using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Zombie : MonoBehaviour
{
    // Velocidaddel zombie hacia la pantalla
    public float velocidadAvance = 5.0f;
    private GameManager GameManager;
    void Start()
    {
        GameManager = Object.FindAnyObjectByType<GameManager>();
    }
    void Update()
    {

        transform.Translate(-Vector3.back * velocidadAvance * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Bullet")) 
        {
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
    }
}
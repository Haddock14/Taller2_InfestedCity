using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (gameManager != null)
            {
                gameManager.ReiniciarJuego();
            }
            else
            {
                Debug.LogError("GameManager no encontrado en la escena.");
            }
        }
    }
}

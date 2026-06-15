using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camino : MonoBehaviour
{

    [SerializeField] private GameObject caminoPrefab;
    private Transform caminoTransform;

    public Transform jugadorTransform;
    public bool jugadorLlego;
    public bool jugadorSeFue;
    public static int cantCaminos = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            jugadorTransform  = jugador.transform;
        }
        else
        {
            Debug.LogError("ERROR: No se ha encontrado un GameObject con la etiqueta 'Player' en la escena.");
            return;
        }

        if (cantCaminos >= 10)
        {
            Debug.Log("Limite de caminos alcanzado");
            return;
        }

        if (caminoPrefab == null) 
        {
            Debug.LogError("ERROR: No se ha asignado el prefab 'Camino' en el Inspector de Unity.");
            return; 
        }

        //caminoPrefab = Resources.Load<GameObject>("Prefabs/Camino");
        GameObject camino = Instantiate(caminoPrefab, new Vector3(0, 0, 10f * cantCaminos), Quaternion.identity);

        caminoTransform = camino.GetComponent<Transform>();

        jugadorLlego = false; 
        jugadorSeFue = false;
        
        cantCaminos++;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorTransform == null)
        {
            Debug.LogError("ERROR: No se ha asignado el Transform del jugador");
            return;
        }
        if (caminoTransform == null)
        {
            caminoTransform = this.transform;
        }

        if (jugadorTransform.position.z > caminoTransform.position.z + 6 && jugadorLlego)
        {
            jugadorSeFue = true;
        }

        if (jugadorLlego && jugadorSeFue)
        {
            caminoTransform.position += new Vector3(0, 0, 10f * cantCaminos);
            jugadorLlego = false;
            jugadorSeFue = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorLlego = true;
        }
    }

    // Cuando se reinicia la escena, se destruyen los caminos y se reinicia el contador
    void OnDestroy()
    {
        cantCaminos = 0;
    }
}

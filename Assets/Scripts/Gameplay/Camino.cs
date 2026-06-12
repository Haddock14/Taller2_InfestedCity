using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camino : MonoBehaviour
{

    private Transform caminoTransform;
    public Transform jugadorTransform;
    public bool jugadorLlego;
    public bool jugadorSeFue;

    // Start is called before the first frame update
    void Start()
    {
        caminoTransform = GetComponent<Transform>();
        jugadorTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        jugadorLlego = false; 
        jugadorSeFue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorTransform.position.z > caminoTransform.position.z + 6 && jugadorLlego)
        {
            jugadorSeFue = true;
        }

        if (jugadorLlego && jugadorSeFue)
        {
            caminoTransform.position += new Vector3(0, 0, 100f);
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
}

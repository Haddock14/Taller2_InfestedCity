using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private int valorMoneda = 1;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SumarMonedas(valorMoneda);
            Debug.Log(" Moneda Recogida");
            Destroy(gameObject);
        }
    }
}

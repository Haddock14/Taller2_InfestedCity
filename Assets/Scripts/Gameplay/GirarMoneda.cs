using UnityEngine;

public class GirarMoneda : MonoBehaviour
{
    // Velocidad de giro
    public float velocidadGiro = 100f;

    void Update()
    {
        // Rota el objeto sobre su eje Y
        transform.Rotate(0, velocidadGiro * Time.deltaTime, 0);
    }
}
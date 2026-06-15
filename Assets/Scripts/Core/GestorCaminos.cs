using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCaminos : MonoBehaviour
{
   [Header("Configuración del Prefab")]
    [SerializeField] private GameObject caminoPrefab; 
    [SerializeField] private float largoDelCamino = 10f;
    [SerializeField] private int maxCaminos = 10;

    private Transform jugadorTransform;
    private List<GameObject> listaCaminos = new List<GameObject>();
    private float proximaPosicionZ = 0f;

    void Start() 
    {
        // Validar el Prefab
        if (caminoPrefab == null) 
        {
            Debug.LogError("ERROR: No se ha asignado el prefab 'Camino' en el Inspector de Unity.");
            return; 
        }

        // Buscar al jugador
        GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObj != null) 
        {
            jugadorTransform = jugadorObj.transform;
        } 
        else 
        {
            Debug.LogError("ERROR: No se encontró un objeto con el Tag 'Player'.");
            return;
        }

        // Generar exactamente el límite de caminos en fila 
        for (int i = 0; i < maxCaminos; i++)
        {
            CrearCamino();
        }
    }

    void Update() 
    {
        if (jugadorTransform == null || listaCaminos.Count == 0)
        {
            return;
        } 

        if (jugadorTransform.position.z > listaCaminos[0].transform.position.z + largoDelCamino)
        {
            MoverCamino();
        }
    }

    void CrearCamino()
    {
        // Instancia cada camino exactamente uno detrás del otro
        GameObject nuevoCamino = Instantiate(caminoPrefab, new Vector3(0, 0, proximaPosicionZ), Quaternion.identity);
        listaCaminos.Add(nuevoCamino);
        
        // Desplazar la coordenada para el siguiente camino
        proximaPosicionZ += largoDelCamino;
    }

    void MoverCamino()
    {
        // Tomar el camino de mas atras
        GameObject caminoViejo = listaCaminos[0]; // se guarda en variable local
        listaCaminos.RemoveAt(0);

        // Moverlo a la nueva posicion del frente
        caminoViejo.transform.position = new Vector3(0, 0, proximaPosicionZ);
        
        // Añadirlo nuevamente al final de la lista para mantener el orden
        listaCaminos.Add(caminoViejo);

        // Actualizar la coordenada para el próximo ciclo
        proximaPosicionZ += largoDelCamino;
    }
}

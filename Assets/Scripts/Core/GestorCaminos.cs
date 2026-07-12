using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCaminos : MonoBehaviour
{
   [Header("Configuración del Prefab")]
    [SerializeField] private GameObject caminoPrefab; 
    [SerializeField] private float largoDelCamino = 10f;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject[] autosPrefabs;
    [SerializeField] private int maxCaminos = 10;
    [SerializeField] private GameObject monedaPrefab;

    [Header("Decoración")]
    [SerializeField] private GameObject[] edificiosPrefabs;
    private Transform jugadorTransform;
    private List<GameObject> listaCaminos = new List<GameObject>();
    private float proximaPosicionZ = 0f;
    private float[] posicionesX = { -3f, 0f, 3f };

    void Start() 
    {
        // Validar el Prefab
        if (caminoPrefab == null) 
        {
            Debug.LogError("ERROR: No se ha asignado el prefab 'Camino' en el Inspector de Unity.");
            return; 
        }
        if (zombiePrefab == null) 
        {
            Debug.LogError("ERROR: No se ha asignado el prefab 'Zombie' en el Inspector de Unity.");
            return; 
        }
        if (autosPrefabs == null || autosPrefabs.Length == 0) 
        {
            Debug.LogError("ERROR: No se han asignado prefabs de autos en el Inspector de Unity.");
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
        GenerarDecoracion(nuevoCamino);
        GenerarEnemigoEnCamino(nuevoCamino);
        listaCaminos.Add(nuevoCamino);
        
        // Desplazar la coordenada para el siguiente camino
        proximaPosicionZ += largoDelCamino;
    }

    void MoverCamino()
    {
        // Tomar el camino de mas atras
        GameObject caminoViejo = listaCaminos[0]; // se guarda en variable local
        listaCaminos.RemoveAt(0);

        foreach (Transform child in caminoViejo.transform)
        {
            if (child.CompareTag("Enemy") || child.CompareTag("Obstacle") || child.CompareTag("Coin")) Destroy(child.gameObject);
        }

        // Moverlo a la nueva posicion del frente
        caminoViejo.transform.position = new Vector3(0, 0, proximaPosicionZ);
        GenerarEnemigoEnCamino(caminoViejo);
        // Añadirlo nuevamente al final de la lista para mantener el orden
        listaCaminos.Add(caminoViejo);

        // Actualizar la coordenada para el próximo ciclo
        proximaPosicionZ += largoDelCamino;

        
    }

    void GenerarEnemigoEnCamino(GameObject camino)
    {
        float xAleatoriaZombie = posicionesX[Random.Range(0, posicionesX.Length)];

        if (Random.value > 0.5f)
        {
            Vector3 posicionRelativa = new Vector3(xAleatoriaZombie, 0.2f, 2f);
            GameObject zombie = Instantiate(zombiePrefab);

            zombie.transform.position = camino.transform.position + posicionRelativa;
            zombie.transform.SetParent(camino.transform); 
            zombie.tag = "Enemy";
        }

        if (posicionesX[0] > xAleatoriaZombie || xAleatoriaZombie > posicionesX[posicionesX.Length - 1])
        {
            xAleatoriaZombie = 100f; // Asignar un valor fuera del rango para asegurar que no se creo al zombie
        }
        GenerarAuto(camino, xAleatoriaZombie);
        GenerarMoneda(camino, xAleatoriaZombie);
    }

    void GenerarAuto(GameObject camino, float xZombie)
    {
        if (Random.value > 0.5f)
        {
            float xAleatoriaAuto = posicionesX[Random.Range(0, posicionesX.Length)];

            if (xZombie != xAleatoriaAuto) // Evitar que el auto y el zombie estén en la misma posición
            {
                Vector3 posicionRelativa = new Vector3(xAleatoriaAuto, 0.1f, 2f);

                GameObject auto = Instantiate(autosPrefabs[Random.Range(0, autosPrefabs.Length)]);
                auto.transform.position = camino.transform.position + posicionRelativa;
                auto.transform.SetParent(camino.transform); 
                auto.tag = "Obstacle";
            }
        }
    }
    void GenerarMoneda(GameObject camino, float xOcupado)
    {
        if (Random.value > 0.5f)
        {
            float xMoneda = posicionesX[Random.Range(0, posicionesX.Length)];

            if (xMoneda != xOcupado)
            {
                Vector3 posicionRelativaMoneda = new Vector3(xMoneda,0.5f,2f);
                Quaternion rotacionMoneda = Quaternion.Euler(90,0,0);
                GameObject moneda = Instantiate(monedaPrefab, camino.transform.position + posicionRelativaMoneda, Quaternion.identity, camino.transform);
                moneda.tag = "Coin";
            }
        }
    }
    public void GenerarDecoracion(GameObject nuevoCamino)
{
    Transform decoracion = nuevoCamino.transform.Find("Decoracion");

    if (decoracion != null)
    {
        foreach (Transform puntoSpawn in decoracion)
        {
            GameObject prefabEdificio = edificiosPrefabs[Random.Range(0, edificiosPrefabs.Length)];
            Quaternion rotacionEdificio = puntoSpawn.rotation * Quaternion.Euler(-90, 0, 90);
            GameObject nuevoEdificio = Instantiate(prefabEdificio, puntoSpawn.position, rotacionEdificio, nuevoCamino.transform);
            nuevoEdificio.transform.localScale = new Vector3(500, 500, 500); // Ajustar la escala del edificio
        }
    }
}
}

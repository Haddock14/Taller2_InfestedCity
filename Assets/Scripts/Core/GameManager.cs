using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI de Monedas, puntaje y menú de muerte")]
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoPuntaje;
    public GameObject menuMuerte;
    private int monedasRecogidas = 0;
    private float puntaje = 0;
    
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        // Actualizar el puntaje basado en el tiempo
        puntaje += Time.deltaTime * 10; // Incrementa el puntaje a razón de 10 puntos por segundo
        textoPuntaje.text = "Puntaje: " + Mathf.FloorToInt(puntaje).ToString();
    }
    // Método para sumar monedas
    public void SumarMonedas(int cantidad)
    {
        monedasRecogidas += cantidad;
        textoMonedas.text = "Monedas: "+ monedasRecogidas.ToString();
        puntaje += 50;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 0f;
        if (menuMuerte != null)
        {
            menuMuerte.SetActive(true);
        }
    }
    public void ClickEnReiniciar()
    {
        Time.timeScale = 1f; // Restaurar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena
    }
}
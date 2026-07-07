using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI de Monedas")]
    public TextMeshProUGUI textoMonedas;
    private int monedasRecogidas = 0;

    void awake()
    {
        instance = this;
    }
    // Método para sumar monedas
    public void SumarMonedas(int cantidad)
    {
        monedasRecogidas += cantidad;
        textoMonedas.text = "Monedas"+ monedasRecogidas.ToString();
    }

    // Método que llamará el Zombie o el Jugador al perder
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena actual
    }
}
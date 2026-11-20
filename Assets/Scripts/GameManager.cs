using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase Singleton que administra el puntaje, las monedas, el menú de inicio, el fin del juego y el audio.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;
    private static bool esReinicio = false; // Recuerda si venimos de presionar "Reiniciar"

    [Header("Interfaz de Usuario (UI)")]
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoMonedas;
    public GameObject menuInicio;
    public GameObject menuDerrota;

    [Header("Puntajes")]
    public Transform transformJugador;
    private int monedasRecolectadas = 0;
    private float posicionInicialZ;
    private bool juegoActivo = false;

    [Header("Audios")]
    public AudioSource musicaFondo;
    public AudioSource musicaDerrota;

    void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        posicionInicialZ = transformJugador.position.z;
        if (menuDerrota != null) menuDerrota.SetActive(false);

        if (esReinicio)
        {
            // Si venimos de reiniciar, arranca directo sin mostrar el menú de inicio
            esReinicio = false;
            IniciarJuego();
        }
        else
        {
            // Primera vez que se abre el juego: muestra el menú de inicio y pausa
            juegoActivo = false;
            Time.timeScale = 0f;
            if (menuInicio != null) menuInicio.SetActive(true);
        }
    }

    /// <summary>
    /// Método asignado al botón "JUGAR" del menú de inicio.
    /// </summary>
    public void IniciarJuego()
    {
        if (menuInicio != null) menuInicio.SetActive(false);
        juegoActivo = true;
        Time.timeScale = 1f;

        if (musicaFondo != null)
        {
            musicaFondo.time = 15f;
            musicaFondo.Play();
        }
    }

    void Update()
    {
        if (juegoActivo && transformJugador != null)
        {
            int puntuacionActual = Mathf.FloorToInt(transformJugador.position.z - posicionInicialZ);
            textoPuntuacion.text = "PUNTUACIÓN: " + puntuacionActual.ToString("D8");
        }
    }

    /// <summary>
    /// Método para sumar monedas desde el script Moneda.cs.
    /// </summary>
    public void SumarMoneda()
    {
        monedasRecolectadas++;
        textoMonedas.text = "MONEDAS: " + monedasRecolectadas.ToString("D2");
    }

    /// <summary>
    /// Se llama cuando el jugador choca con un dinosaurio o vehículo.
    /// </summary>
    public void GameOver()
    {
        juegoActivo = false;
        Time.timeScale = 0f;
        if (menuDerrota != null) menuDerrota.SetActive(true);

        if (musicaFondo != null) musicaFondo.Stop();
        if (musicaDerrota != null) musicaDerrota.Play();
    }

    /// <summary>
    /// Método asignado al botón "Reiniciar Partida" en la UI.
    /// </summary>
    public void ReiniciarJuego()
    {
        esReinicio = true; // Avisa a la escena recargada que se salte el menú de inicio
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
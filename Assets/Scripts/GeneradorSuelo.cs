using UnityEngine;

/// <summary>
/// Genera de forma infinita las plataformas hacia adelante.
/// </summary>
public class GeneradorSuelo : MonoBehaviour
{
    public GameObject prefabPlataforma; // El suelo base a instanciar
    public Transform jugador;
    public float longitudPlataforma = 30f; // El largo en Z de tu modelo de plataforma
    public int cantidadPlataformasInicial = 15; // Entre 10 y 30 según enunciado

    private float posicionSpawnZ = 0f;

    void Start()
    {
        // Instanciar las plataformas iniciales al comienzo de la partida
        for (int i = 0; i < cantidadPlataformasInicial; i++)
        {
            GenerarPlataforma();
        }
    }

    void Update()
    {
        // Si el jugador avanza lo suficiente, generar una nueva plataforma adelante
        if (jugador.position.z - longitudPlataforma > (posicionSpawnZ - (cantidadPlataformasInicial * longitudPlataforma)))
        {
            GenerarPlataforma();
        }
    }

    /// <summary>
    /// Instancia una plataforma nueva al final del camino actual.
    /// </summary>
    private void GenerarPlataforma()
    {
        Instantiate(prefabPlataforma, new Vector3(0, 0, posicionSpawnZ), Quaternion.identity);
        posicionSpawnZ += longitudPlataforma; // Mueve el punto de generación hacia adelante
    }
}
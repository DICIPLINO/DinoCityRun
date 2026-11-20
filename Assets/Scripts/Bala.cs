using UnityEngine;

/// <summary>
/// Clase que controla el comportamiento del proyectil lanzado por el jugador.
/// </summary>
public class Bala : MonoBehaviour
{
    public float velocidadBala = 25f; // Velocidad del proyectil
    public float tiempoDeVida = 2f; // Tiempo antes de autodestruirse para ahorrar memoria

    void Start()
    {
        // Se destruye automáticamente después del tiempo establecido
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        // Se mueve siempre recto hacia adelante en el mundo, sin desviarse
        transform.Translate(Vector3.forward * velocidadBala * Time.deltaTime, Space.World);
    
    }

    // Nota: Las colisiones con los enemigos y obstáculos se gestionan en los scripts de ellos.
}
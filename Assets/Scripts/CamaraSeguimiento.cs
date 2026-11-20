using UnityEngine;

/// <summary>
/// Clase que hace que la cámara siga al jugador únicamente en el eje Z (hacia adelante),
/// manteniendo su altura (Y) y su posición horizontal (X) fijas.
/// </summary>
public class CamaraSeguimiento : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    public Transform jugador; // Referencia al jugador que la cámara debe seguir

    private float distanciaZ; // Separación inicial en el eje Z entre la cámara y el jugador

    void Start()
    {
        // Calculamos la diferencia de posición en Z al inicio para mantenerla siempre igual
        if (jugador != null)
        {
            distanciaZ = transform.position.z - jugador.position.z;
        }
    }

    // Usamos LateUpdate para que la cámara se mueva justo después de que el jugador ya se haya movido
    void LateUpdate()
    {
        if (jugador != null)
        {
            // Mantenemos la posición X e Y actuales de la cámara, y solo actualizamos la Z
            transform.position = new Vector3(transform.position.x, transform.position.y, jugador.position.z + distanciaZ);
        }
    }
}
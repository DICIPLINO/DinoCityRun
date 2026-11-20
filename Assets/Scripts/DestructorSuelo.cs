using UnityEngine;

/// <summary>
/// Clase encargada de destruir las plataformas (y sus elementos hijos) que ya han quedado 
/// atrás del jugador, optimizando la memoria del juego.
/// </summary>
public class DestructorSuelo : MonoBehaviour
{
    /// <summary>
    /// Detecta cuando una plataforma entra en el área del colisionador invisible que dejamos atrás.
    /// </summary>
    /// <param name="other">El colisionador del objeto que acaba de entrar.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Suelo"))
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
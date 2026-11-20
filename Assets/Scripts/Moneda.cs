using UnityEngine;

/// <summary>
/// Clase que controla el comportamiento de la moneda recolectable:
/// gira visualmente, aumenta el contador de monedas al chocar con el jugador y se destruye.
/// </summary>
public class Moneda : MonoBehaviour
{
    [Header("Configuración Visual")]
    public float velocidadRotacion = 100f; // Velocidad a la que gira la moneda sobre su eje

    [Header("Audio")]
    public AudioClip sonidoMoneda;

    void Update()
    {
        // Hace que la moneda gire constantemente para destacarse en el camino
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Detecta cuando el jugador entra en el colisionador de la moneda para recolectarla.
    /// </summary>
    /// <param name="other">El colisionador del objeto que tocó la moneda.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con el jugador, suma una moneda al GameManager y se destruye
        if (other.CompareTag("Player"))
        {
            GameManager.Instancia.SumarMoneda();

            if (sonidoMoneda != null)
            {
                Vector3 posSonido = Camera.main != null ? Camera.main.transform.position : transform.position;
                AudioSource.PlayClipAtPoint(sonidoMoneda, posSonido, 1f);
            }

            Destroy(gameObject);
        }
    }
}
using UnityEngine;

/// <summary>
/// Clase que define el comportamiento del dinosaurio: movimiento infinito hacia el jugador,
/// sistema de vida con múltiples disparos y sonidos de impacto y muerte.
/// </summary>
public class Enemigo : MonoBehaviour
{
    public float velocidad = 8f;       // Velocidad a la que avanza contra el jugador
    public int vida = 3;               // Cantidad de disparos necesarios para morir

    [Header("Audio")]
    public AudioClip sonidoImpacto;    // Sonido cada vez que recibe una bala
    public AudioSource audioMuerte;    // Sonido al morir definitivamente

    void Update()
    {
        // Space.World asegura que siempre avance hacia el jugador, sin importar la rotación del modelo
        transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si choca con el jugador, el jugador pierde la partida
        if (other.CompareTag("Player"))
        {
            GameManager.Instancia.GameOver();
        }
        // Si recibe un proyectil (bala) del jugador
        else if (other.CompareTag("Bala"))
        {
            Destroy(other.gameObject); // Destruye la bala
            vida--;                    // Resta 1 punto de vida

            Vector3 posSonido = Camera.main != null ? Camera.main.transform.position : transform.position;

            if (vida <= 0)
            {
                // Reproduce el sonido de muerte y destruye al dinosaurio
                if (audioMuerte != null && audioMuerte.clip != null)
                {
                    AudioSource.PlayClipAtPoint(audioMuerte.clip, posSonido, 1f);
                }
                Destroy(gameObject);
            }
            else
            {
                // Aún sigue vivo: reproduce el sonido de impacto/daño
                if (sonidoImpacto != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoImpacto, posSonido, 1f);
                }
            }
        }
    }
}
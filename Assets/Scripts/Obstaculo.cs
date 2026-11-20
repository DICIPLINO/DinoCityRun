using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Si choca con el jugador, se activa el menú de derrota
        if (other.CompareTag("Player"))
        {
            GameManager.Instancia.GameOver();
        }
        // Si recibe un proyectil del jugador, solo se destruye la bala (el auto resiste)
        else if (other.CompareTag("Bala"))
        {
            Destroy(other.gameObject);
        }
    }
}
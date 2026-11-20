using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Clase encargada de aparecer objetos (obstáculos, enemigos o monedas) en los 3 carriles de la plataforma,
/// aumentando progresivamente la cantidad de amenazas conforme avanza el tiempo de partida.
/// </summary>
public class Spawneador : MonoBehaviour
{
    [Header("Puntos de Aparición (Carriles)")]
    public Transform[] puntosCarril; // 3 EmptyObjects ubicados en cada carril

    [Header("Objetos a Instanciar")]
    public GameObject[] dinosaurios;
    public GameObject[] vehiculos;
    public GameObject prefabMoneda;

    [Header("Dificultad Progresiva")]
    public float tiempoParaAumentar = 20f;     // A los 20s empiezan a salir hasta 2 objetos por plataforma
    public float tiempoDificultadAlta = 45f;   // A los 45s casi siempre salen 2 objetos (más peligrosos)

    void Start()
    {
        if (puntosCarril.Length == 0) return;

        float tiempoActual = Time.timeSinceLevelLoad;

        // 1. Determinar cuántos objetos spawnear en esta plataforma (máximo 2 para no bloquear los 3 carriles)
        int cantidadASpawnear = 1;

        if (tiempoActual >= tiempoDificultadAlta)
        {
            cantidadASpawnear = 2; // Siempre 2 objetos en dificultad alta
        }
        else if (tiempoActual >= tiempoParaAumentar)
        {
            // 50% de probabilidad de que salgan 2 objetos
            cantidadASpawnear = Random.value < 0.5f ? 2 : 1;
        }

        // Asegurar que no pida más objetos que carriles disponibles
        cantidadASpawnear = Mathf.Min(cantidadASpawnear, puntosCarril.Length);

        // 2. Crear una lista de carriles disponibles para no repetir carril
        List<int> carrilesDisponibles = new List<int>();
        for (int i = 0; i < puntosCarril.Length; i++)
        {
            carrilesDisponibles.Add(i);
        }

        // 3. Instanciar los objetos elegidos
        for (int i = 0; i < cantidadASpawnear; i++)
        {
            int indiceLista = Random.Range(0, carrilesDisponibles.Count);
            int carrilElegido = carrilesDisponibles[indiceLista];
            carrilesDisponibles.RemoveAt(indiceLista); // Quita el carril para no encimar nada

            Transform puntoSpawn = puntosCarril[carrilElegido];
            GameObject objetoAInstanciar = ElegirObjetoSegunTiempo(tiempoActual);

            if (objetoAInstanciar != null)
            {
                GameObject instancia = Instantiate(objetoAInstanciar, puntoSpawn.position, objetoAInstanciar.transform.rotation);
                instancia.transform.SetParent(transform, true);
            }
        }
    }

    /// <summary>
    /// Elige aleatoriamente entre moneda, vehículo o dinosaurio, reduciendo la probabilidad
    /// de monedas y aumentando la de enemigos/obstáculos conforme avanza la partida.
    /// </summary>
    private GameObject ElegirObjetoSegunTiempo(float tiempo)
    {
        // Al inicio: 35% moneda. En dificultad alta: baja hasta 15% moneda (85% dinosaurios/autos)
        float probMoneda = Mathf.Lerp(0.35f, 0.15f, tiempo / 60f);
        float dado = Random.value;

        if (dado < probMoneda)
        {
            return prefabMoneda;
        }
        else if (dado < probMoneda + (1f - probMoneda) * 0.5f)
        {
            if (vehiculos.Length > 0)
                return vehiculos[Random.Range(0, vehiculos.Length)];
        }
        else
        {
            if (dinosaurios.Length > 0)
                return dinosaurios[Random.Range(0, dinosaurios.Length)];
        }

        return prefabMoneda;
    }
}
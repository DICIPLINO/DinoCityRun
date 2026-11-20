using UnityEngine;

public class JugadorController : MonoBehaviour
{
    [Header("Atributos de Movimiento")]
    public float velocidadAvance = 10f; // Velocidad inicial
    public float velocidadMaxima = 25f; // Tope máximo para que siga siendo jugable
    public float aceleracion = 0.3f;    // Cuánto aumenta la velocidad por segundo
    public float fuerzaSalto = 7f;
    public float distanciaCarril = 3f;

    private int carrilActual = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
    private Rigidbody rb;
    private bool enElSuelo = true;

    [Header("Atributos de Disparo")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float tiempoMinimoDisparo = 0.5f;
    private float temporizadorDisparo = 0f;

    [Header("Audio")]
    public AudioSource audioDisparo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 0. Dificultad progresiva: aumenta la velocidad poco a poco hasta el límite
        if (velocidadAvance < velocidadMaxima)
        {
            velocidadAvance += aceleracion * Time.deltaTime;
        }

        // 1. Movimiento forzado hacia adelante
        transform.Translate(Vector3.forward * velocidadAvance * Time.deltaTime);

        // 2. Cambio de carriles (Teclas A y D)
        if (Input.GetKeyDown(KeyCode.D) && carrilActual < 2)
        {
            carrilActual++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && carrilActual > 0)
        {
            carrilActual--;
        }

        Vector3 posicionObjetivo = new Vector3((carrilActual - 1) * distanciaCarril, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, 10f * Time.deltaTime);

        // 3. Salto (Tecla Espacio)
        if (Input.GetKeyDown(KeyCode.Space) && enElSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enElSuelo = false;
        }

        // 4. Mecánica de disparo (Tecla F)
        temporizadorDisparo -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && temporizadorDisparo <= 0f)
        {
            Disparar();
            temporizadorDisparo = tiempoMinimoDisparo;
        }
    }

    private void Disparar()
    {
        GameObject nuevaBala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);

        // La bala siempre viajará 25 unidades/s más rápido que la velocidad actual del jugador
        Bala scriptBala = nuevaBala.GetComponent<Bala>();
        if (scriptBala != null)
        {
            scriptBala.velocidadBala = velocidadAvance + 25f;
        }

        if (audioDisparo != null) audioDisparo.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
        }
    }
}
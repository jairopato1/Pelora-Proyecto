using UnityEngine;

public class ChampionFinal : MonoBehaviour
{
    [Header("Interfaces de la Escena")]
    // Aquí arrastraremos la pantalla de Victoria del Canvas
    public GameObject pantallaVictoria; 
    // Aquí arrastraremos el texto flotante de "Presiona E"
    public GameObject textoEntregar; 

    private bool jugadorCerca = false;
    private MinerController mineroCerca;

    void Start()
    {
        // Nos aseguramos de que la victoria empiece oculta
        if (pantallaVictoria != null) pantallaVictoria.SetActive(false);
        
        // El texto de "Presiona E" empieza apagado hasta que te acerques
        if (textoEntregar != null) textoEntregar.SetActive(false);
    }

    void Update()
    {
        // Si el jugador está al lado del hongo Y presiona la tecla E
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            // Verificamos si el minero de verdad tiene la moneda en su bolsillo
            if (mineroCerca != null && mineroCerca.monedasRecolectadas >= 1)
            {
                Debug.Log("¡Ofrenda entregada al Champion!");

                // Encendemos la pantalla de Victoria épica del Troll
                if (pantallaVictoria != null) pantallaVictoria.SetActive(true);
                
                // Apagamos el texto flotante para que quede limpia la pantalla
                if (textoEntregar != null) textoEntregar.SetActive(false);
                
                // Congelamos el juego porque ya ganaste
                Time.timeScale = 0f; 
            }
            else
            {
                Debug.Log("El Champion te mira feo porque no tienes la moneda.");
            }
        }
    }

    // Detecta cuando el minero entra al territorio del hongo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            mineroCerca = collision.GetComponent<MinerController>();
            
            // Hacemos aparecer el letrero flotante
            if (textoEntregar != null) textoEntregar.SetActive(true);
        }
    }

    // Detecta si el minero se aleja sin presionar nada
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            mineroCerca = null;
            
            // Escondemos el letrero flotante
            if (textoEntregar != null) textoEntregar.SetActive(false);
        }
    }
}
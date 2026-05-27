using UnityEngine;

public class FrutaPoder : MonoBehaviour
{
    [Header("Interfaz Visual")]
    // Aquí arrastraremos el texto que creaste para que el código lo pueda apagar/prender
    public GameObject textoFlotante; 

    private bool jugadorCerca = false;
    private MinerController mineroCerca;

    void Start()
    {
        // Apenas empieza el nivel, nos aseguramos de que el texto esté apagado
        if (textoFlotante != null)
        {
            textoFlotante.SetActive(false);
        }
    }

    void Update()
    {
        // Si el jugador está tocando la calabaza Y presiona la tecla E...
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (mineroCerca != null)
            {
                mineroCerca.tieneDobleSalto = true; 
                Debug.Log("¡Te comiste la fruta! Doble Salto activado.");
            }
            
            // La fruta desaparece
            Destroy(gameObject); 
        }
    }

    // Cuando el minero ENTRA a la zona de la calabaza
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            mineroCerca = collision.GetComponent<MinerController>();
            
            // Aparece el texto mágicamente
            if (textoFlotante != null)
            {
                textoFlotante.SetActive(true);
            }
        }
    }

    // Cuando el minero SALE de la zona sin presionar la E
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            mineroCerca = null;

            // Ocultamos el texto para dejar la pantalla limpia
            if (textoFlotante != null)
            {
                textoFlotante.SetActive(false);
            }
        }
    }
}
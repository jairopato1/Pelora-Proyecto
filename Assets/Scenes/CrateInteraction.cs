using UnityEngine;

public class CrateInteraction : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject textoFlotante; 
    
    [Header("Referencias del Jefe")]
    // Aquí arrastraremos al Boss desde el Inspector
    public BossController jefeDelNivel; 
    
    private bool jugadorCerca = false;
    private MinerController minero;

    void Update()
    {
        // Si el jugador está en la zona y presiona E
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            // Le damos el poder de saltar al minero
            if (minero != null)
            {
                minero.puedeSaltar = true;
            }
            
            // ¡DESPERTAMOS AL BOSS!
            if (jefeDelNivel != null)
            {
                jefeDelNivel.estaActivo = true;
                Debug.Log("¡El Boss se ha despertado!");
            }
            
            // Apagamos el texto por seguridad antes de destruir la caja
            if (textoFlotante != null)
            {
                textoFlotante.SetActive(false); 
            }
            
            // Rompemos la caja
            Destroy(gameObject);
        }
    }

    // Usamos OnTrigger porque ahora detectamos la zona grande "fantasma"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            minero = collision.GetComponent<MinerController>();
            
            // Mostramos el texto sin parpadeos
            if (textoFlotante != null)
            {
                textoFlotante.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            minero = null;
            
            // Ocultamos el texto si el jugador se aleja
            if (textoFlotante != null)
            {
                textoFlotante.SetActive(false);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class PortalSalida : MonoBehaviour
{
    // Escribe aquí el nombre exacto de tu escena final desde el Inspector
    public string nombreEscenaFinal = "EscenaFinal"; 

    // Se activa automáticamente cuando el minero entra al círculo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Pasando a la escena final de Pelora!");
            
            // Nos aseguramos de que el tiempo corra normal en la siguiente escena
            Time.timeScale = 1f; 
            
            // Cargamos la escena de cierre
            SceneManager.LoadScene(nombreEscenaFinal);
        }
    }
}
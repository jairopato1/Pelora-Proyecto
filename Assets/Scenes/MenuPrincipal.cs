using UnityEngine;
using UnityEngine.SceneManagement; // Esta línea es fundamental para poder cambiar de escena

public class MenuPrincipal : MonoBehaviour
{
    // Esta es la función que va a ejecutar el botón al hacer clic
    public void EmpezarJuego()
    {
        // Pon aquí entre comillas el nombre exacto de la escena de tu nivel.
        // Si tu escena principal se llama "JUEGO", déjalo así:
        SceneManager.LoadScene("JUEGO"); 
    }
}
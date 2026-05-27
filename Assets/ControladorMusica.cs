using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    // Esto asegura que solo exista un reproductor de música en todo el juego
    private static ControladorMusica instancia;

    void Awake()
    {
        // Si no hay música sonando, nos guardamos y nos volvemos inmortales
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // ¡Esta es la línea que lo hace viajar entre escenas!
        }
        // Si ya hay una música sonando (porque moriste y volviste al menú), destruimos el clon
        else
        {
            Destroy(gameObject);
        }
    }
}
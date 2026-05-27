using UnityEngine;
using UnityEngine.SceneManagement; // ¡Esta línea es obligatoria para cambiar niveles!

public class ControladorNivel : MonoBehaviour
{
    // Esta es la función que conectaremos al botón
    public void ReiniciarEscenaActual()
    {
        // 1. Descongelamos el tiempo (súper importante para que el juego vuelva a correr)
        Time.timeScale = 1f; 
        
        // 2. Buscamos cómo se llama la escena actual y la volvemos a cargar
        string nombreEscenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nombreEscenaActual);
    }
}
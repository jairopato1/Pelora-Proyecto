using UnityEngine;

public class ControlPausa : MonoBehaviour
{
    [Header("Elementos de Interfaz")]
    public GameObject panelPausa;   // El panel gris que tiene el botón Play
    public GameObject botonPausa;   // El botón de Pausa de la esquina
    
    private bool juegoPausado = false;

    void Update()
    {
        // También puedes pausar con la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado) ReanudarJuego();
            else PausarJuego();
        }
    }

    public void PausarJuego()
    {
        panelPausa.SetActive(true);  // Aparece la pantalla gris y el botón Play
        if (botonPausa != null) botonPausa.SetActive(false); // Escondemos el botón de Pausa
        
        Time.timeScale = 0f;         // Congelamos el tiempo
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false); // Escondemos la pantalla gris
        if (botonPausa != null) botonPausa.SetActive(true);  // Volvemos a mostrar el botón de Pausa
        
        Time.timeScale = 1f;         // Descongelamos el tiempo
        juegoPausado = false;
    }
}
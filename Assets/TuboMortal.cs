using UnityEngine;

public class TuboMortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MinerController minero = collision.GetComponent<MinerController>();
            
            if (minero != null)
            {
                // Le quitamos todas las vidas de un golpe
                minero.vidas = 0; 
                // Llamamos a la función que activa la pantalla de Game Over
                minero.RecibirDano(); 
            }
        }
    }
}
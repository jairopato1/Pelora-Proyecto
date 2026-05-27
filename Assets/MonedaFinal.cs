using UnityEngine;

public class MonedaFinal : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidadRotacion = 200f; 
    
    // Aquí guardaremos tu archivo de sonido
    public AudioClip sonidoRecolectar; 

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MinerController minero = collision.GetComponent<MinerController>();
            
            if (minero != null)
            {
                minero.monedasRecolectadas++; 
                
                // Hace sonar la moneda justo antes de desaparecer
                if (sonidoRecolectar != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoRecolectar, Camera.main.transform.position, 1f);
                }
            }
            
            Destroy(gameObject); 
        }
    }
}
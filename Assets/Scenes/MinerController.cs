using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class MinerController : MonoBehaviour
{
    public float velocidad = 5f; 
    public float fuerzaSalto = 7f;
    public bool puedeSaltar = true; 
    public int vidas = 2; 

    [Header("Interfaz UI")]
    public GameObject pantallaGameOver; 
    public TextMeshProUGUI textoVidasMinero; 

    [Header("Reinicio de Juego")]
    public string nombreEscenaInicial = "EscribeAquiElNombre"; 

    [Header("Mecánicas Finales")]
    public bool tieneDobleSalto = false; 
    private bool hizoDobleSalto = false; 
    public int monedasRecolectadas = 0;  

    [Header("Sonidos")]
    public AudioSource fuentePasos; 
    // --- NUEVO: Casilla para el sonido de Game Over ---
    public AudioClip sonidoGameOver; 

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Time.timeScale = 1f; 

        if (pantallaGameOver != null) pantallaGameOver.SetActive(false);
        
        if (textoVidasMinero != null)
        {
            textoVidasMinero.gameObject.SetActive(true);
            textoVidasMinero.text = "Vidas: " + vidas;
        }
    }

    void Update()
    {
        if (estaMuerto) return;

        float movimientoX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);

        if (Mathf.Abs(movimientoX) > 0.1f)
        {
            animator.SetBool("estaCaminando", true); 
            if (movimientoX > 0) spriteRenderer.flipX = false;
            else if (movimientoX < 0) spriteRenderer.flipX = true;

            if (fuentePasos != null && !fuentePasos.isPlaying && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
            {
                fuentePasos.Play();
            }
        }
        else 
        {
            animator.SetBool("estaCaminando", false); 
            if (fuentePasos != null) fuentePasos.Stop();
        }

        if (puedeSaltar && Input.GetButtonDown("Jump")) 
        {
            if (fuentePasos != null) fuentePasos.Stop(); 

            if (Mathf.Abs(rb.linearVelocity.y) < 0.1f) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
                hizoDobleSalto = false; 
            }
            else if (tieneDobleSalto && !hizoDobleSalto) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto); 
                hizoDobleSalto = true; 
            }
        }
    }

    public void RecibirDano()
    {
        if (estaMuerto) return; 

        vidas--;
        
        if (textoVidasMinero != null) textoVidasMinero.text = "Vidas: " + vidas;
        
        if (vidas <= 0) StartCoroutine(TemporizadorMuerte());
    }

    System.Collections.IEnumerator TemporizadorMuerte()
    {
        estaMuerto = true;
        rb.linearVelocity = Vector2.zero; 
        if (fuentePasos != null) fuentePasos.Stop(); 

        // --- NUEVO: Reproduce el sonido de derrota ---
        if (sonidoGameOver != null)
        {
            AudioSource.PlayClipAtPoint(sonidoGameOver, Camera.main.transform.position, 1f);
        }

        if (pantallaGameOver != null) pantallaGameOver.SetActive(true);
        Time.timeScale = 0f; 
        
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(nombreEscenaInicial);
    }
}
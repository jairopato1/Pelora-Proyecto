using UnityEngine;
using TMPro; // VITAL para poder controlar el texto flotante

public class BossController : MonoBehaviour
{
    public int vidasBoss = 2;
    public float fuerzaSaltoVertical = 7f;   
    public float fuerzaSaltoHorizontal = 4f; 
    
    [Header("Mecánica de Aturdimiento")]
    public float tiempoAturdido = 0.5f; 
    private bool estaAturdido = false;
    private float contadorAturdimiento;

    [Header("Activación")]
    public bool estaActivo = false; 
    private bool vidaInicialMostrada = false; // Interruptor para encender el texto al despertar

    [Header("Interfaz UI y Portal")]
    public GameObject pantallaVictoria; 
    public GameObject circuloGrisPortal; 
    
    public TMP_Text textoVidaFlotante; 

    // --- NUEVO: Casilla para arrastrar el audio del Troll ---
    [Header("Efectos de Sonido")]
    public AudioClip sonidoMuerteTroll; 

    private Rigidbody2D rb;
    private float temporizadorSalto;
    private SpriteRenderer spriteRenderer; 
    private Transform transformJugador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null) transformJugador = jugador.transform;
        
        temporizadorSalto = Random.Range(0.5f, 1f);

        if (pantallaVictoria != null) pantallaVictoria.SetActive(false);
        if (circuloGrisPortal != null) circuloGrisPortal.SetActive(false);

        // Apagamos el texto flotante al inicio porque el Troll está congelado
        if (textoVidaFlotante != null)
        {
            textoVidaFlotante.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!estaActivo) return;

        // ¡Se despertó! Encendemos el texto "2/2" de inmediato
        if (!vidaInicialMostrada && textoVidaFlotante != null)
        {
            textoVidaFlotante.gameObject.SetActive(true);
            textoVidaFlotante.text = vidasBoss + "/2";
            vidaInicialMostrada = true;
        }

        if (estaAturdido)
        {
            contadorAturdimiento -= Time.deltaTime; 
            if (contadorAturdimiento <= 0)
            {
                estaAturdido = false; 
                temporizadorSalto = Random.Range(0.5f, 1.2f); 
            }
            return; 
        }

        temporizadorSalto -= Time.deltaTime;

        if (temporizadorSalto <= 0)
        {
            PrepararSaltoInteligente();
            temporizadorSalto = Random.Range(0.5f, 1.5f); 
        }
    }

    void PrepararSaltoInteligente()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            float movimientoX = 0f;

            if (transformJugador != null)
            {
                float distanciaX = transformJugador.position.x - transform.position.x;
                if (distanciaX > 0.5f) 
                {
                    movimientoX = fuerzaSaltoHorizontal;
                    if (spriteRenderer != null) spriteRenderer.flipX = true; 
                }
                else if (distanciaX < -0.5f) 
                {
                    movimientoX = -fuerzaSaltoHorizontal;
                    if (spriteRenderer != null) spriteRenderer.flipX = false; 
                }
            }

            rb.linearVelocity = new Vector2(movimientoX, fuerzaSaltoVertical);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!estaActivo) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (estaAturdido) return;

            float alturaJugador = collision.transform.position.y;
            float alturaBoss = transform.position.y;

            if (alturaJugador > alturaBoss + 0.5f) 
            {
                vidasBoss--;
                
                // Actualizamos el texto flotante al recibir el pisotón
                if (textoVidaFlotante != null)
                {
                    textoVidaFlotante.text = vidasBoss + "/2"; 
                }
                
                if (vidasBoss <= 0)
                {
                    // --- NUEVO: REPRODUCE EL SONIDO ANTES DE DESTRUIRSE ---
                    if (sonidoMuerteTroll != null)
                    {
                        AudioSource.PlayClipAtPoint(sonidoMuerteTroll, transform.position, 1f);
                    }
                    // ------------------------------------------------------

                    if (pantallaVictoria != null) pantallaVictoria.SetActive(true);
                    if (circuloGrisPortal != null) circuloGrisPortal.SetActive(true);
                    Destroy(gameObject); 
                    return; 
                }

                estaAturdido = true;
                contadorAturdimiento = tiempoAturdido;
                rb.linearVelocity = Vector2.zero; 

                Rigidbody2D rbJugador = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rbJugador != null)
                {
                    float ladoRebote = (collision.transform.position.x - transform.position.x) > 0 ? 5f : -5f;
                    rbJugador.linearVelocity = new Vector2(ladoRebote, 7f); 
                }
            }
            else 
            {
                MinerController minero = collision.gameObject.GetComponent<MinerController>();
                if (minero != null) minero.RecibirDano();
            }
        }
    }
}
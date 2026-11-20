using UnityEngine;

public class SonidoPelota : MonoBehaviour
{
    [Header("Clips de Audio (MP3 / WAV)")]
    public AudioClip sonidoGolpePalo;    // Sonido seco cuando la impulsa el palo
    public AudioClip sonidoChoquePared;  // Sonido de rebote contra paredes u obstáculos

    [Header("Ajustes de Volumen y Sensibilidad")]
    [Range(0f, 1f)]
    public float volumenMaximo = 1f;
    public float fuerzaMinimaChoque = 0.4f; // Ignora roces diminutos contra el pasto
    public float saltoVelocidadPalo = 0.25f; // Detecta cuando el palo la acelera

    private AudioSource altavoz;
    private Rigidbody rb;
    private float velocidadAnterior = 0f;
    private float ultimoSonidoPalo = -1f;
    private float tiempoInicio;

    void Start()
    {
        altavoz = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        tiempoInicio = Time.time;
    }

    void Update()
    {
        if (rb == null || altavoz == null) return;

        // Ignora el primer segundo cuando cae al pasto al arrancar
        if (Time.time - tiempoInicio < 1.0f)
        {
            velocidadAnterior = rb.linearVelocity.magnitude;
            return;
        }

        // 1. DETECTA EL GOLPE DEL PALO (como tu palo empuja por código sin choque físico)
        float velocidadActual = rb.linearVelocity.magnitude;
        float aceleracion = velocidadActual - velocidadAnterior;

        if (aceleracion >= saltoVelocidadPalo && (Time.time - ultimoSonidoPalo >= 0.4f))
        {
            ultimoSonidoPalo = Time.time;
            if (sonidoGolpePalo != null)
            {
                // Calcula el volumen según qué tan fuerte le pegaste
                float volumenProporcional = Mathf.Clamp(aceleracion / 3f, 0.2f, volumenMaximo);
                altavoz.PlayOneShot(sonidoGolpePalo, volumenProporcional);
            }
        }

        velocidadAnterior = velocidadActual;
    }

    // 2. DETECTA CHOQUES FÍSICOS CONTRA PAREDES, BORDES Y OBSTÁCULOS
    private void OnCollisionEnter(Collision collision)
    {
        if (altavoz == null || sonidoChoquePared == null) return;

        // Ignora el impacto inicial al caer al suelo al comenzar
        if (Time.time - tiempoInicio < 1.0f) return;

        // Mide la fuerza real del impacto
        float fuerzaImpacto = collision.relativeVelocity.magnitude;

        if (fuerzaImpacto >= fuerzaMinimaChoque)
        {
            // Si choca despacio suena bajito; si rebota fuerte suena al máximo
            float volumenImpacto = Mathf.Clamp(fuerzaImpacto / 5f, 0.15f, volumenMaximo);
            altavoz.PlayOneShot(sonidoChoquePared, volumenImpacto);
        }
    }
}

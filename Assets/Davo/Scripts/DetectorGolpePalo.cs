using UnityEngine;

public class ContadorGolpePalo : MonoBehaviour
{
    [Header("Conexión con el Cerebro")]
    public LogicaContador logicaContador;

    [Header("Arrastrá acá tu Pelota")]
    public Rigidbody rbPelota;

    [Header("Ajustes de Golpe")]
    public float saltoDeVelocidad = 0.25f; // Cuánto tiene que acelerar de golpe para contar como palazo
    public float tiempoEntreGolpes = 0.7f; // Tiempo mínimo entre un golpe y otro

    private float velocidadAnterior = 0f;
    private float ultimoGolpe = -10f;
    private float tiempoInicio;

    void Start()
    {
        tiempoInicio = Time.time;

        if (rbPelota == null)
        {
            GameObject obj = GameObject.FindWithTag("Player");
            if (obj == null) obj = GameObject.Find("Pelota");
            if (obj != null) rbPelota = obj.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (rbPelota == null || logicaContador == null) return;

        // Ignora los primeros 1.2 segundos mientras la pelota cae al pasto al iniciar
        if (Time.time - tiempoInicio < 1.2f)
        {
            velocidadAnterior = rbPelota.linearVelocity.magnitude;
            return;
        }

        float velocidadActual = rbPelota.linearVelocity.magnitude;

        // Calcula cuánto aceleró la pelota en este instante (detecta el impacto aunque no haya frenado del todo)
        float aceleracionRepentina = velocidadActual - velocidadAnterior;

        if (aceleracionRepentina >= saltoDeVelocidad)
        {
            if (Time.time - ultimoGolpe >= tiempoEntreGolpes)
            {
                ultimoGolpe = Time.time;
                logicaContador.SumarGolpe();
                Debug.Log("¡GOLPE REAL DETECTADO! Aceleración: " + aceleracionRepentina);
            }
        }

        // Guarda la velocidad para compararla en el siguiente frame
        velocidadAnterior = velocidadActual;
    }
}

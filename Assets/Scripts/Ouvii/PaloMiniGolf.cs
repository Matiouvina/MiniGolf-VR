using UnityEngine;

public class PaloMiniGolf : MonoBehaviour
{
    public float fuerza = 10f;
    
    private Vector3 posicionAnterior;
    private float velocidadActualPalo;

    void Start()
    {
        posicionAnterior = transform.position;
    }

    void Update()
    {
        Vector3 desplazamiento = transform.position - posicionAnterior;
        velocidadActualPalo = desplazamiento.magnitude / Time.deltaTime;
        posicionAnterior = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pelota"))
        {
            Rigidbody rbPelota = other.GetComponent<Rigidbody>();
            
            if (rbPelota != null)
            {
                Vector3 direccionImpacto = other.transform.position - transform.position;
                Vector3 direccionFisica = new Vector3(direccionImpacto.x, 0f, direccionImpacto.z).normalized;

                float fuerzaFinal = velocidadActualPalo * fuerza;

                rbPelota.linearVelocity = direccionFisica * fuerzaFinal;
            }
        }
    }
}

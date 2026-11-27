using UnityEngine;

public class ControlPelota : MonoBehaviour
{
    [Header("Configuración del Puente")]
    [Tooltip("Fuerza extra que ayuda a la pelota a subir el puente cuando lo toca")]
    public float impulsoSubida = 8f;
    [Tooltip("Tag que debe tener el objeto puente")]
    public string tagDelPuente = "Puente";
    public string tagDelJugador = "Player";

    private Rigidbody rb;
    private bool enElPuente = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        IgnorarColisionesConJugador();
    }

    void IgnorarColisionesConJugador()
    {
        Collider colliderPelota = GetComponent<Collider>();
        GameObject jugador = GameObject.FindGameObjectWithTag(tagDelJugador);

        if (jugador != null && colliderPelota != null)
        {
            Collider[] collidersJugador = jugador.GetComponentsInChildren<Collider>();
            foreach (Collider col in collidersJugador)
            {
                Physics.IgnoreCollision(colliderPelota, col, true);
            }
        }
    }

    // Detecta automáticamente cuando la pelota toca el puente
    void OnCollisionStay(Collision colision)
    {
        if (colision.gameObject.CompareTag(tagDelPuente))
        {
            enElPuente = true;

            // Obtiene la inclinación del puente en el punto donde está la pelota
            Vector3 normalSuelo = colision.contacts[0].normal;
            
            // Si el suelo está inclinado (subiendo o bajando el puente) y la pelota se está moviendo
            if (normalSuelo.y < 0.99f && rb.linearVelocity.magnitude > 0.2f)
            {
                // Calcula la dirección hacia adelante siguiendo la inclinación del puente
                Vector3 direccionAvance = Vector3.ProjectOnPlane(rb.linearVelocity.normalized, normalSuelo).normalized;

                // Si está subiendo (la dirección apunta hacia arriba en Y), le da un empujón extra
                if (direccionAvance.y > 0.05f)
                {
                    rb.AddForce(direccionAvance * impulsoSubida, ForceMode.Acceleration);
                }
            }
        }
    }

    void OnCollisionExit(Collision colision)
    {
        if (colision.gameObject.CompareTag(tagDelPuente))
        {
            enElPuente = false;
        }
    }
}

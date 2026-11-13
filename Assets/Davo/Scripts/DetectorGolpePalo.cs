using UnityEngine;

public class ContadorGolpePalo : MonoBehaviour
{
    [Header("Conexión con el Cerebro")]
    public LogicaContador logicaContador;

    [Header("Ajustes de Detección")]
    public float tiempoEntreGolpes = 0.6f; // Sube un poquito esto para que un roce suave no cuente doble
    private float ultimoGolpe = -10f;

    // Detecta golpe fuerte o normal
    private void OnCollisionEnter(Collision collision)
    {
        VerificarYContar(collision.gameObject);
    }

    // Detecta roce suave o empujón lento en VR
    private void OnCollisionStay(Collision collision)
    {
        VerificarYContar(collision.gameObject);
    }

    // Detecta si el palo entra suave en modo Trigger
    private void OnTriggerEnter(Collider other)
    {
        VerificarYContar(other.gameObject);
    }

    // Detecta si el palo roza suavemente en modo Trigger
    private void OnTriggerStay(Collider other)
    {
        VerificarYContar(other.gameObject);
    }

    private void VerificarYContar(GameObject objetoTocado)
    {
        Transform raiz = objetoTocado.transform.root;
        bool esPelota = objetoTocado.name.ToLower().Contains("pelota") || 
                        raiz.name.ToLower().Contains("pelota") ||
                        objetoTocado.CompareTag("Player") || 
                        raiz.CompareTag("Player");

        if (esPelota)
        {
            if (Time.time - ultimoGolpe >= tiempoEntreGolpes)
            {
                if (logicaContador != null)
                {
                    ultimoGolpe = Time.time;
                    logicaContador.SumarGolpe();
                }
            }
        }
    }
}

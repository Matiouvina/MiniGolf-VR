using UnityEngine;

public class ContadorGolpePalo : MonoBehaviour
{
    [Header("Conexión con el Cerebro")]
    public LogicaContador logicaContador; // Casilla para arrastrar el objeto LogicaContador

    private void OnCollisionEnter(Collision collision)
    {
        // Revisa si el palo chocó físicamente contra la pelota blanca
        if (collision.gameObject.name.ToLower().Contains("pelota") || collision.gameObject.CompareTag("Player"))
        {
            if (logicaContador != null)
            {
                logicaContador.SumarGolpe(); // Le da el aviso a la lógica
            }
            else
            {
                Debug.LogError("ERROR: Te falta arrastrar el objeto LogicaContador a la casilla del palo.");
            }
        }
    }
}

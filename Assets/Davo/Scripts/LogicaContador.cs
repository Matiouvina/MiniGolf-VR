using UnityEngine;
using TMPro;

public class LogicaContador : MonoBehaviour
{
    public TextMeshProUGUI textoGolpes;
    private int cantidadGolpes = 0;

    void Start()
    {
        cantidadGolpes = 0;
        ActualizarTexto();
        Debug.Log("INICIO: Contador en 00. Texto asignado: " + (textoGolpes != null ? textoGolpes.name : "NINGUNO"));
    }

    public void SumarGolpe()
    {
        cantidadGolpes++;
        ActualizarTexto();
        Debug.Log("¡GOLPE SUMADO! Valor actual en memoria: " + cantidadGolpes);
    }

    void ActualizarTexto()
    {
        if (textoGolpes != null)
        {
            textoGolpes.text = cantidadGolpes.ToString("00");
            textoGolpes.ForceMeshUpdate(); // Obliga a Unity a redibujar el número neón al instante
        }
        else
        {
            Debug.LogError("ERROR: No hay ningún texto arrastrado en LogicaContador.");
        }
    }
}

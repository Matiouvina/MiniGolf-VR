using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegacionJuego : MonoBehaviour
{
    // Conectar al botón JUGAR (en el Menú) y al botón SIGUIENTE (en los 4 mapas)
    public void IrASiguienteEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int totalEscenas = SceneManager.sceneCountInBuildSettings;

        if (escenaActual + 1 < totalEscenas)
        {
            SceneManager.LoadScene(escenaActual + 1);
        }
        else
        {
            // Si terminaste el último mapa, vuelve al menú principal (índice 0)
            SceneManager.LoadScene(0);
        }
    }

    // Conectar al botón REINICIAR (flecha circular)
    public void ReiniciarMapaActual()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);
    }

    // Conectar al botón MENÚ (casita)
    public void VolverAlInicio()
    {
        SceneManager.LoadScene(0);
    }
}

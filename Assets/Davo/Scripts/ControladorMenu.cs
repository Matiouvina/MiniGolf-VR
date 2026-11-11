using UnityEngine;
using UnityEngine.SceneManagement; // Esta línea es clave para poder controlar las escenas

public class ControladorMenu : MonoBehaviour
{
    // Esta función se ejecutará al pulsar el botón
    public void Jugar()
    {
        // Opción A: Carga la escena que esté inmediatamente después en la lista
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Opción B (Descomentá si preferís usar el nombre exacto de tu escena):
        // SceneManager.LoadScene("NombreDeTuEscenaDeJuego");
    }
}

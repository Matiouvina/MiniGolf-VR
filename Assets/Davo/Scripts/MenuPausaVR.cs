using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuPausaVR : MonoBehaviour
{
    [Header("Panel Visual")]
    public GameObject panelPausa; // Arrastrá acá tu cartel vertical de Pausa

    [Header("Controles VR")]
    public InputActionReference botonR2Abrir;   // Asigná XRI RightHand Interaction / Activate (R2)
    public InputActionReference botonXSeleccionar; // Asigná XRI LeftHand / Primary Button (X)

    [Header("Posicionar frente al jugador")]
    public Transform camaraJugadorVR; // Arrastrá la Main Camera
    public float distanciaFrenteAlJugador = 1.5f;

    private bool estaPausado = false;

    void Start()
    {
        if (panelPausa != null) panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnEnable()
    {
        if (botonR2Abrir != null && botonR2Abrir.action != null)
        {
            botonR2Abrir.action.Enable();
            botonR2Abrir.action.performed += AlPresionarR2;
        }

        if (botonXSeleccionar != null && botonXSeleccionar.action != null)
        {
            botonXSeleccionar.action.Enable();
            botonXSeleccionar.action.performed += AlPresionarX;
        }
    }

    void OnDisable()
    {
        if (botonR2Abrir != null && botonR2Abrir.action != null)
            botonR2Abrir.action.performed -= AlPresionarR2;

        if (botonXSeleccionar != null && botonXSeleccionar.action != null)
            botonXSeleccionar.action.performed -= AlPresionarX;
    }

    // 1. CON R2 ABRE Y CIERRA EL MENÚ DE PAUSA
    private void AlPresionarR2(InputAction.CallbackContext context)
    {
        AlternarPausa();
    }

    // 2. CON LA X HACE CLIC EN EL BOTÓN QUE ESTÁS APUNTANDO
    private void AlPresionarX(InputAction.CallbackContext context)
    {
        if (!estaPausado) return;

        // Busca qué objeto de la interfaz estás apuntando actualmente con el láser
        GameObject objetoApuntado = EventSystem.current.currentSelectedGameObject;

        // Si el EventSystem tiene un botón iluminado/apuntado, ejecuta su clic
        if (objetoApuntado != null)
        {
            Button boton = objetoApuntado.GetComponent<Button>();
            if (boton != null)
            {
                boton.onClick.Invoke();
            }
        }
    }

    void Update()
    {
        // Tecla Escape para probar en monitor sin casco
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }

    public void AlternarPausa()
    {
        estaPausado = !estaPausado;

        if (panelPausa != null)
        {
            panelPausa.SetActive(estaPausado);

            if (estaPausado && camaraJugadorVR != null)
            {
                Vector3 direccionPlana = new Vector3(camaraJugadorVR.forward.x, 0f, camaraJugadorVR.forward.z).normalized;
                panelPausa.transform.position = camaraJugadorVR.position + direccionPlana * distanciaFrenteAlJugador;
                panelPausa.transform.rotation = Quaternion.LookRotation(direccionPlana);
            }
        }

        Time.timeScale = estaPausado ? 0f : 1f;
    }

    // --- FUNCIONES PARA CONECTAR EN ON CLICK () DE TUS 3 BOTONES ---

    public void Reanudar()
    {
        estaPausado = false;
        if (panelPausa != null) panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

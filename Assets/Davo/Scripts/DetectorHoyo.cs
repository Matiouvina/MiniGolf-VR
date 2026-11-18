using UnityEngine;
using System.Collections;
using TMPro;

public class DetectorHoyo : MonoBehaviour
{
    [Header("Carteles de Resultado (Stickers)")]
    public GameObject pantallaGanaste; 
    public GameObject pantallaDerrota; 

    [Header("Textos de Victoria")]
    public TextMeshProUGUI golpesFinalText; 
    public TextMeshProUGUI tiempoFinalText; 

    [Header("Textos de Derrota")]
    public TextMeshProUGUI golpesDerrotaText;
    public TextMeshProUGUI tiempoDerrotaText;

    [Header("Elementos de Arriba a Desaparecer")]
    public GameObject contadorGolpes;
    public GameObject golpesText;
    public GameObject contadorTiempo;
    public GameObject tiempoText;

    [Header("Condiciones del Juego (Modificables)")]
    public int golpesMaximos = 30;
    public float tiempoMaximoSegundos = 90f;

    [Header("Botones de Fin de Nivel")]
    public GameObject menu;
    public GameObject reiniciar;
    public GameObject siguiente;

    private bool juegoTerminado = false;
    private float tiempoActual = 0f;

    void Update()
    {
        if (juegoTerminado) return;

        // 1. CONTROL DE TIEMPO LÍMITE
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoMaximoSegundos)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false));
        }

        // 2. CONTROL DE GOLPES MÁXIMOS MODIFICABLES
        if (golpesText != null)
        {
            TextMeshProUGUI textoUI = golpesText.GetComponent<TextMeshProUGUI>();
            if (textoUI != null)
            {
                int golpesActuales;
                if (int.TryParse(textoUI.text, out golpesActuales))
                {
                    if (golpesActuales >= golpesMaximos)
                    {
                        juegoTerminado = true;
                        StartCoroutine(MostrarResultadoConRetraso(false));
                    }
                }
            }
        }

        // Teclas de prueba rápida
        if (Input.GetKeyDown(KeyCode.G))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(false));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        if (other.name.ToLower().Contains("pelota") || other.CompareTag("Player"))
        {
            juegoTerminado = true;
            StartCoroutine(MostrarResultadoConRetraso(true));
        }
    }

    IEnumerator MostrarResultadoConRetraso(bool gano)
    {
        yield return new WaitForSeconds(1f);

        // Obtiene los valores reales antes de ocultar la interfaz de arriba
        string golpesObtenidos = (golpesText != null) ? golpesText.GetComponent<TextMeshProUGUI>().text : "00";
        string tiempoObtenido = (tiempoText != null) ? tiempoText.GetComponent<TextMeshProUGUI>().text : "00:00";

        // Rellena los textos de Victoria
        if (golpesFinalText != null) golpesFinalText.text = golpesObtenidos;
        if (tiempoFinalText != null) tiempoFinalText.text = tiempoObtenido;

        // Rellena los textos de Derrota
        if (golpesDerrotaText != null) golpesDerrotaText.text = golpesObtenidos;
        if (tiempoDerrotaText != null) tiempoDerrotaText.text = tiempoObtenido;

        // Oculta los elementos de arriba
        if (contadorGolpes != null) contadorGolpes.SetActive(false);
        if (golpesText != null) golpesText.SetActive(false);
        if (contadorTiempo != null) contadorTiempo.SetActive(false);
        if (tiempoText != null) tiempoText.SetActive(false);

        // Activa el cartel y los botones correspondientes
        if (gano && pantallaGanaste != null)
        {
            pantallaGanaste.SetActive(true);
            if (menu != null) menu.SetActive(true);
            if (reiniciar != null) reiniciar.SetActive(true);
            if (siguiente != null) siguiente.SetActive(true);
        }
        else if (!gano && pantallaDerrota != null)
        {
            pantallaDerrota.SetActive(true);
            if (menu != null) menu.SetActive(true);
            if (reiniciar != null) reiniciar.SetActive(true);
            if (siguiente != null) siguiente.SetActive(false);
        }
    }
}

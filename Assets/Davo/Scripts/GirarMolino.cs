using UnityEngine;

public class GirarMolino : MonoBehaviour
{
    [Header("Velocidad de giro en cada eje")]
    public float velocidadX = 0f;
    public float velocidadY = 0f;
    public float velocidadZ = 35f; // Probá primero con 35 en Z

    void Update()
    {
        transform.Rotate(velocidadX * Time.deltaTime, velocidadY * Time.deltaTime, velocidadZ * Time.deltaTime);
    }
}

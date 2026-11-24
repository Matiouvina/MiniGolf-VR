using UnityEngine;

public class ControlPelota : MonoBehaviour
{
    public float velocidad = 15f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moverHorizontal = Input.GetAxis("Horizontal");
        float moverVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moverHorizontal, 0.0f, moverVertical);

        rb.AddForce(movimiento * velocidad);
    }
}

using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        float moverX = Input.GetAxis("Horizontal");
        float moverZ = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(moverX, 0f, moverZ);
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }
}

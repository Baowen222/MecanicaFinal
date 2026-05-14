using UnityEngine;

public class ZonaFuerza : MonoBehaviour
{
    public Vector3 direccionFuerza = new Vector3(0f, 0f, 30f);

    void OnTriggerStay(Collider otro)
    {
        Rigidbody rb = otro.attachedRigidbody;
        if (rb != null)
        {
            // Requisito: fuerza continua con AddForce
            // Aquí usamos AddForce como fuerza continua
            rb.AddForce(direccionFuerza);
        }
    }
}

using UnityEngine;

public class ZonaFuerza : MonoBehaviour
{
    public Vector3 direccionFuerza = new Vector3(0f, 0f, 20f);

    void OnTriggerStay(Collider otro)
    {
        Rigidbody rb = otro.attachedRigidbody;
        if (rb != null)
        {
            // Aqui se usa AddForce como fuerza continua (sin ForceMode.Impulse).
            rb.AddForce(direccionFuerza);
        }
    }
}

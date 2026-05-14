using UnityEngine;

public class PhysicsButton : MonoBehaviour
{
    public DoorOpener puerta;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caja") || other.CompareTag("Bola"))
        {
            if (puerta != null)
            {
                puerta.AbrirPuerta();
            }
        }
    }
}

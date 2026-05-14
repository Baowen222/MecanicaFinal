using UnityEngine;

public class BotonFisico : MonoBehaviour
{
    public AbridorPuerta puerta;

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Caja") || otro.CompareTag("Bola"))
        {
            if (puerta != null)
            {
                puerta.AbrirPuerta();
            }
        }
    }
}

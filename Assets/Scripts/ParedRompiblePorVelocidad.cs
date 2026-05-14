using UnityEngine;

public class ParedRompiblePorVelocidad : MonoBehaviour
{
    public float velocidadMinimaParaRomper = 9f;

    void OnCollisionEnter(Collision colision)
    {
        if (!colision.gameObject.CompareTag("Bola"))
        {
            return;
        }

        Rigidbody rb = colision.rigidbody;
        if (rb == null)
        {
            return;
        }

        // Aqui se comprueba la velocidad con rb.velocity.magnitude.
        if (rb.velocity.magnitude >= velocidadMinimaParaRomper)
        {
            gameObject.SetActive(false);
        }
    }
}

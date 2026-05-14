using UnityEngine;

public class ParedRompiblePorVelocidad : MonoBehaviour
{
    public float velocidadMinimaParaRomper = 6f;

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
        if (rb.linearVelocity.magnitude >= velocidadMinimaParaRomper)
        {
            gameObject.SetActive(false);
        }
    }
}

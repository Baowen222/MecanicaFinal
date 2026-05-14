using UnityEngine;

public class ParedRompiblePorVelocidad : MonoBehaviour
{
    public float velocidadMinimaParaRomper = 5.5f;
    public ControlVictoria controlVictoria;

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

        // Requisito: comprobamos la velocidad del Rigidbody
        // Aquí comprobamos la velocidad de la bola
        if (rb.linearVelocity.magnitude >= velocidadMinimaParaRomper)
        {
            if (controlVictoria != null)
            {
                controlVictoria.paredRota = true;
                controlVictoria.ComprobarVictoria();
            }

            Debug.Log("Pared rompible destruida");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("La bola iba demasiado lenta");
        }
    }
}

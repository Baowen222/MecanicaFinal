using UnityEngine;

public class BotonFisico : MonoBehaviour
{
    public AbridorPuerta puerta;
    public ControlVictoria controlVictoria;
    public Renderer renderBoton;
    bool yaActivado;

    void OnTriggerEnter(Collider otro)
    {
        if (yaActivado)
        {
            return;
        }

        if (otro.CompareTag("Caja") || otro.CompareTag("Bola"))
        {
            yaActivado = true;
            transform.position += new Vector3(0f, -0.08f, 0f);

            if (renderBoton != null)
            {
                renderBoton.material.color = new Color(0.9f, 0.55f, 0.1f);
            }

            if (controlVictoria != null)
            {
                controlVictoria.botonActivado = true;
                controlVictoria.ComprobarVictoria();
            }

            if (puerta != null)
            {
                puerta.AbrirPuerta();
            }

            Debug.Log("Botón activado");
        }
    }
}

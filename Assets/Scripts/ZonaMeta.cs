using UnityEngine;

public class ZonaMeta : MonoBehaviour
{
    public ControlVictoria controlVictoria;

    void OnTriggerEnter(Collider otro)
    {
        if (!otro.CompareTag("Player") || controlVictoria == null)
        {
            return;
        }

        controlVictoria.jugadorEnMeta = true;
        controlVictoria.ComprobarVictoria();

        if (!controlVictoria.juegoGanado)
        {
            controlVictoria.MostrarFaltanPruebas();
        }
    }
}

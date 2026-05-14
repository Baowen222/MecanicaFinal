using UnityEngine;
using UnityEngine.UI;

public class ControlVictoria : MonoBehaviour
{
    public bool paredRota;
    public bool botonActivado;
    public bool puertaAbierta;
    public bool jugadorEnMeta;
    public bool juegoGanado;

    public GameObject panelVictoria;
    public Text textoAviso;
    public float tiempoAviso = 2f;

    float tiempoRestanteAviso;

    void Start()
    {
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }

        if (textoAviso != null)
        {
            textoAviso.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (textoAviso != null && textoAviso.gameObject.activeSelf)
        {
            tiempoRestanteAviso -= Time.deltaTime;
            if (tiempoRestanteAviso <= 0f)
            {
                textoAviso.gameObject.SetActive(false);
            }
        }
    }

    public void ComprobarVictoria()
    {
        if (paredRota && botonActivado && puertaAbierta && jugadorEnMeta)
        {
            juegoGanado = true;
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
            }
            Debug.Log("Has ganado");
        }
    }

    public void MostrarFaltanPruebas()
    {
        Debug.Log("Primero completa todas las pruebas");

        if (textoAviso != null)
        {
            textoAviso.text = "Primero completa todas las pruebas";
            textoAviso.gameObject.SetActive(true);
            tiempoRestanteAviso = tiempoAviso;
        }
    }
}

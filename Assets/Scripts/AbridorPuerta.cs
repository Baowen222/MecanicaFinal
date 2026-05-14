using UnityEngine;

public class AbridorPuerta : MonoBehaviour
{
    public bool puertaAbierta;
    public float alturaAbierta = 6f;
    public float velocidadApertura = 3f;
    public ControlVictoria controlVictoria;

    Vector3 posicionCerrada;
    Vector3 posicionAbierta;
    bool avisoEnviado;

    void Start()
    {
        posicionCerrada = transform.position;
        posicionAbierta = posicionCerrada + Vector3.up * alturaAbierta;
    }

    void Update()
    {
        if (puertaAbierta)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionAbierta, velocidadApertura * Time.deltaTime);

            if (!avisoEnviado && Vector3.Distance(transform.position, posicionAbierta) < 0.05f)
            {
                avisoEnviado = true;
                if (controlVictoria != null)
                {
                    controlVictoria.puertaAbierta = true;
                    controlVictoria.ComprobarVictoria();
                }
                Debug.Log("Puerta abierta");
            }
        }
    }

    public void AbrirPuerta()
    {
        puertaAbierta = true;
    }
}

using UnityEngine;

public class AbridorPuerta : MonoBehaviour
{
    public bool puertaAbierta = false;
    public float alturaAbierta = 4f;
    public float velocidadApertura = 2f;

    private Vector3 posicionCerrada;
    private Vector3 posicionAbierta;

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
        }
    }

    public void AbrirPuerta()
    {
        puertaAbierta = true;
    }
}

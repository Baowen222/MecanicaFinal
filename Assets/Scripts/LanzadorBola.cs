using UnityEngine;

public class LanzadorBola : MonoBehaviour
{
    public GameObject prefabBola;
    public Transform puntoSalida;
    public float fuerzaLanzamiento = 12f;
    public float tiempoDeVidaBola = 6f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            LanzarBola();
        }
    }

    public void LanzarBola()
    {
        if (prefabBola == null || puntoSalida == null)
        {
            return;
        }

        GameObject nuevaBola = Instantiate(prefabBola, puntoSalida.position, Quaternion.identity);
        nuevaBola.SetActive(true);

        Rigidbody rbBola = nuevaBola.GetComponent<Rigidbody>();
        if (rbBola != null)
        {
            rbBola.linearVelocity = Vector3.zero;
            rbBola.angularVelocity = Vector3.zero;
            // Requisito: impulso con ForceMode.Impulse
            // Aquí usamos impulso para lanzar la bola
            rbBola.AddForce(transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }

        BolaTemporal bolaTemporal = nuevaBola.GetComponent<BolaTemporal>();
        if (bolaTemporal == null)
        {
            bolaTemporal = nuevaBola.AddComponent<BolaTemporal>();
        }
        bolaTemporal.tiempoDeVida = tiempoDeVidaBola;
    }
}

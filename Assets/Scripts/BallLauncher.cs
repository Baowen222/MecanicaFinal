using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject bolaPrefab;
    public Transform puntoSalida;
    public float fuerzaLanzamiento = 12f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LanzarBola();
        }
    }

    void LanzarBola()
    {
        if (bolaPrefab == null || puntoSalida == null)
        {
            return;
        }

        GameObject nuevaBola = Instantiate(bolaPrefab, puntoSalida.position, Quaternion.identity);
        Rigidbody rb = nuevaBola.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Aqui usamos impulso para que salga disparada
            rb.AddForce(transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
}

using UnityEngine;

public class LanzadorBola : MonoBehaviour
{
    public GameObject prefabBola;
    public Transform puntoSalida;
    public float fuerzaLanzamiento = 12f;
    public float tiempoDeVidaBola = 6f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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

        Rigidbody rigidbodyBola = nuevaBola.GetComponent<Rigidbody>();
        if (rigidbodyBola != null)
        {
            rigidbodyBola.velocity = Vector3.zero;
            rigidbodyBola.angularVelocity = Vector3.zero;
            rigidbodyBola.AddForce(transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }

        Destroy(nuevaBola, tiempoDeVidaBola);
    }
}

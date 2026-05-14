using UnityEngine;

public class LanzadorBola : MonoBehaviour
{
    public GameObject prefabBola;
    public Transform puntoSalida;
    public float fuerzaLanzamiento = 12f;

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
        Rigidbody rigidbodyBola = nuevaBola.GetComponent<Rigidbody>();

        if (rigidbodyBola != null)
        {
            rigidbodyBola.AddForce(transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
}

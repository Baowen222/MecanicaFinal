using UnityEngine;

public class CamaraSeguimientoSimple : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 desplazamiento = new Vector3(0f, 7f, -10f);

    void LateUpdate()
    {
        if (objetivo == null)
        {
            return;
        }

        transform.position = objetivo.position + desplazamiento;
        transform.LookAt(objetivo.position + Vector3.up * 1.2f);
    }
}

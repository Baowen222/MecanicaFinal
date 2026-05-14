using UnityEngine;

public class ZonaMeta : MonoBehaviour
{
    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("Prueba completada");
        }
    }
}

using UnityEngine;

public class GoalZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Prueba completada");
        }
    }
}

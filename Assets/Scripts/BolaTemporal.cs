using UnityEngine;

public class BolaTemporal : MonoBehaviour
{
    public float tiempoDeVida = 6f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
}

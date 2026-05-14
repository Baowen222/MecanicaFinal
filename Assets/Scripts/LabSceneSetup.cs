using UnityEngine;

public class LabSceneSetup : MonoBehaviour
{
    void Start()
    {
        CrearJugador();
        CrearSala();
        CrearPuertaYBoton();
        CrearCajas();
        CrearMeta();
    }

    void CrearJugador()
    {
        GameObject jugador = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        jugador.name = "Jugador";
        jugador.tag = "Player";
        jugador.transform.position = new Vector3(0f, 1f, -8f);
        jugador.AddComponent<MovimientoJugadorSimple>();

        GameObject camara = Camera.main.gameObject;
        camara.transform.SetParent(jugador.transform);
        camara.transform.localPosition = new Vector3(0f, 1.5f, -4f);
        camara.transform.localEulerAngles = new Vector3(15f, 0f, 0f);

        GameObject puntoSalida = new GameObject("PuntoSalidaBola");
        puntoSalida.transform.SetParent(jugador.transform);
        puntoSalida.transform.localPosition = new Vector3(0f, 1f, 1.2f);

        GameObject bolaPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bolaPrefab.name = "BolaPrefab";
        bolaPrefab.tag = "Bola";
        bolaPrefab.transform.position = new Vector3(100f, 100f, 100f);
        bolaPrefab.SetActive(false);
        Rigidbody rb = bolaPrefab.AddComponent<Rigidbody>();
        rb.mass = 1f;

        Material matBola = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        matBola.color = Color.blue;
        bolaPrefab.GetComponent<Renderer>().material = matBola;

        LanzadorBola lanzador = jugador.AddComponent<LanzadorBola>();
        lanzador.prefabBola = bolaPrefab;
        lanzador.puntoSalida = puntoSalida.transform;
    }

    void CrearSala()
    {
        Material gris = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        gris.color = new Color(0.7f, 0.7f, 0.7f);

        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Plane);
        suelo.name = "Suelo";
        suelo.transform.localScale = new Vector3(2f, 1f, 2f);

        CrearPared("ParedNorte", new Vector3(0f, 2f, 20f), new Vector3(40f, 4f, 1f), gris);
        CrearPared("ParedSur", new Vector3(0f, 2f, -20f), new Vector3(40f, 4f, 1f), gris);
        CrearPared("ParedEste", new Vector3(20f, 2f, 0f), new Vector3(1f, 4f, 40f), gris);
        CrearPared("ParedOeste", new Vector3(-20f, 2f, 0f), new Vector3(1f, 4f, 40f), gris);
    }

    void CrearPared(string nombre, Vector3 posicion, Vector3 escala, Material mat)
    {
        GameObject pared = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pared.name = nombre;
        pared.transform.position = posicion;
        pared.transform.localScale = escala;
        pared.GetComponent<Renderer>().material = mat;
    }

    void CrearPuertaYBoton()
    {
        GameObject puerta = GameObject.CreatePrimitive(PrimitiveType.Cube);
        puerta.name = "Puerta";
        puerta.transform.position = new Vector3(0f, 2f, 10f);
        puerta.transform.localScale = new Vector3(3f, 4f, 1f);

        Material grisOscuro = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        grisOscuro.color = new Color(0.25f, 0.25f, 0.25f);
        puerta.GetComponent<Renderer>().material = grisOscuro;

        AbridorPuerta abridorPuerta = puerta.AddComponent<AbridorPuerta>();

        GameObject boton = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boton.name = "BotonFisico";
        boton.transform.position = new Vector3(0f, 0.2f, 4f);
        boton.transform.localScale = new Vector3(2f, 0.3f, 2f);

        Material amarillo = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        amarillo.color = Color.yellow;
        boton.GetComponent<Renderer>().material = amarillo;

        BoxCollider col = boton.GetComponent<BoxCollider>();
        col.isTrigger = true;

        BotonFisico botonFisico = boton.AddComponent<BotonFisico>();
        botonFisico.puerta = abridorPuerta;
    }

    void CrearCajas()
    {
        Material grisCaja = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        grisCaja.color = new Color(0.6f, 0.6f, 0.6f);

        for (int i = 0; i < 3; i++)
        {
            GameObject caja = GameObject.CreatePrimitive(PrimitiveType.Cube);
            caja.name = "Caja" + (i + 1);
            caja.tag = "Caja";
            caja.transform.position = new Vector3(-2f + (i * 2f), 0.8f, 0f);
            caja.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            caja.GetComponent<Renderer>().material = grisCaja;
            caja.AddComponent<Rigidbody>();
        }
    }

    void CrearMeta()
    {
        GameObject meta = GameObject.CreatePrimitive(PrimitiveType.Cube);
        meta.name = "Meta";
        meta.transform.position = new Vector3(0f, 0.5f, 15f);
        meta.transform.localScale = new Vector3(4f, 1f, 2f);

        Material verde = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        verde.color = Color.green;
        meta.GetComponent<Renderer>().material = verde;

        BoxCollider col = meta.GetComponent<BoxCollider>();
        col.isTrigger = true;
        meta.AddComponent<ZonaMeta>();
    }
}

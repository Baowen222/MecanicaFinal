using UnityEditor;
using UnityEngine;

public static class CrearEscenaPhysicsTestLab
{
    [MenuItem("Tools/Physics Test Lab/Crear Escena Base")]
    public static void CrearEscenaBase()
    {
        GameObject raizAnterior = GameObject.Find("PhysicsTestLab_Escena");
        if (raizAnterior != null)
        {
            Object.DestroyImmediate(raizAnterior);
        }

        GameObject raiz = new GameObject("PhysicsTestLab_Escena");

        Material matSuelo = ObtenerOCrearMaterial("Assets/Materials/Mat_Suelo.mat", new Color(0.55f, 0.55f, 0.55f));
        Material matBola = ObtenerOCrearMaterial("Assets/Materials/Mat_Bola_Azul.mat", Color.blue);
        Material matCaja = ObtenerOCrearMaterial("Assets/Materials/Mat_Caja_Gris.mat", new Color(0.65f, 0.65f, 0.65f));
        Material matBoton = ObtenerOCrearMaterial("Assets/Materials/Mat_Boton_Amarillo.mat", Color.yellow);
        Material matPuerta = ObtenerOCrearMaterial("Assets/Materials/Mat_Puerta_Oscura.mat", new Color(0.2f, 0.2f, 0.2f));
        Material matMeta = ObtenerOCrearMaterial("Assets/Materials/Mat_Meta_Verde.mat", Color.green);

        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Plane);
        suelo.name = "Suelo";
        suelo.transform.position = Vector3.zero;
        suelo.transform.localScale = new Vector3(2f, 1f, 2f);
        suelo.GetComponent<Renderer>().sharedMaterial = matSuelo;
        suelo.transform.SetParent(raiz.transform);

        CrearPared("Pared_Izquierda", new Vector3(-20f, 2f, 0f), new Vector3(1f, 4f, 40f), matCaja, raiz.transform);
        CrearPared("Pared_Derecha", new Vector3(20f, 2f, 0f), new Vector3(1f, 4f, 40f), matCaja, raiz.transform);
        CrearPared("Pared_Fondo", new Vector3(0f, 2f, 20f), new Vector3(40f, 4f, 1f), matCaja, raiz.transform);

        GameObject puerta = GameObject.CreatePrimitive(PrimitiveType.Cube);
        puerta.name = "Puerta";
        puerta.transform.position = new Vector3(0f, 2f, 10f);
        puerta.transform.localScale = new Vector3(3f, 4f, 1f);
        puerta.GetComponent<Renderer>().sharedMaterial = matPuerta;
        puerta.transform.SetParent(raiz.transform);
        AbridorPuerta abridorPuerta = puerta.AddComponent<AbridorPuerta>();

        GameObject boton = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boton.name = "Boton_Fisico";
        boton.transform.position = new Vector3(0f, 0.2f, 4f);
        boton.transform.localScale = new Vector3(2f, 0.3f, 2f);
        boton.GetComponent<Renderer>().sharedMaterial = matBoton;
        BoxCollider colBoton = boton.GetComponent<BoxCollider>();
        colBoton.isTrigger = true;
        BotonFisico botonFisico = boton.AddComponent<BotonFisico>();
        botonFisico.puerta = abridorPuerta;
        boton.transform.SetParent(raiz.transform);

        GameObject meta = GameObject.CreatePrimitive(PrimitiveType.Cube);
        meta.name = "Meta";
        meta.transform.position = new Vector3(0f, 0.5f, 15f);
        meta.transform.localScale = new Vector3(4f, 1f, 2f);
        meta.GetComponent<Renderer>().sharedMaterial = matMeta;
        BoxCollider colMeta = meta.GetComponent<BoxCollider>();
        colMeta.isTrigger = true;
        meta.AddComponent<ZonaMeta>();
        meta.transform.SetParent(raiz.transform);

        for (int i = 1; i <= 3; i++)
        {
            GameObject caja = GameObject.CreatePrimitive(PrimitiveType.Cube);
            caja.name = "Caja_Fisica_" + i;
            caja.tag = "Caja";
            caja.transform.position = new Vector3(-2f + ((i - 1) * 2f), 0.8f, 0f);
            caja.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            caja.GetComponent<Renderer>().sharedMaterial = matCaja;
            caja.AddComponent<Rigidbody>();
            caja.transform.SetParent(raiz.transform);
        }

        GameObject bola = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bola.name = "Bola_Lanzable";
        bola.tag = "Bola";
        bola.transform.position = new Vector3(0f, 1f, -6f);
        bola.GetComponent<Renderer>().sharedMaterial = matBola;
        Rigidbody rbBola = bola.AddComponent<Rigidbody>();
        rbBola.mass = 1f;
        bola.transform.SetParent(raiz.transform);

        GameObject jugador = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        jugador.name = "Jugador";
        jugador.tag = "Player";
        jugador.transform.position = new Vector3(0f, 1f, -8f);
        jugador.AddComponent<MovimientoJugadorSimple>();
        LanzadorBola lanzador = jugador.AddComponent<LanzadorBola>();
        lanzador.prefabBola = bola;

        GameObject puntoSalida = new GameObject("Punto_Salida_Bola");
        puntoSalida.transform.SetParent(jugador.transform);
        puntoSalida.transform.localPosition = new Vector3(0f, 1f, 1.2f);
        lanzador.puntoSalida = puntoSalida.transform;

        Rigidbody rbJugador = jugador.AddComponent<Rigidbody>();
        rbJugador.constraints = RigidbodyConstraints.FreezeRotation;
        jugador.transform.SetParent(raiz.transform);

        Camera camara = Camera.main;
        if (camara == null)
        {
            GameObject objetoCamara = new GameObject("Main Camera");
            camara = objetoCamara.AddComponent<Camera>();
            objetoCamara.tag = "MainCamera";
        }
        camara.transform.position = new Vector3(0f, 14f, -18f);
        camara.transform.rotation = Quaternion.Euler(25f, 0f, 0f);

        Light luzDireccional = Object.FindObjectOfType<Light>();
        if (luzDireccional == null || luzDireccional.type != LightType.Directional)
        {
            GameObject objetoLuz = new GameObject("Directional Light");
            luzDireccional = objetoLuz.AddComponent<Light>();
            luzDireccional.type = LightType.Directional;
        }
        luzDireccional.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        Selection.activeGameObject = raiz;
        Debug.Log("Escena base de Physics Test Lab creada correctamente.");
    }

    static void CrearPared(string nombre, Vector3 posicion, Vector3 escala, Material material, Transform padre)
    {
        GameObject pared = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pared.name = nombre;
        pared.transform.position = posicion;
        pared.transform.localScale = escala;
        pared.GetComponent<Renderer>().sharedMaterial = material;
        pared.transform.SetParent(padre);
    }

    static Material ObtenerOCrearMaterial(string rutaMaterial, Color color)
    {
        Material material = AssetDatabase.LoadAssetAtPath<Material>(rutaMaterial);
        if (material != null)
        {
            return material;
        }

        if (!AssetDatabase.IsValidFolder("Assets/Materials"))
        {
            AssetDatabase.CreateFolder("Assets", "Materials");
        }

        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
        {
            shader = Shader.Find("Standard");
        }

        material = new Material(shader);
        material.color = color;
        AssetDatabase.CreateAsset(material, rutaMaterial);
        AssetDatabase.SaveAssets();
        return material;
    }
}

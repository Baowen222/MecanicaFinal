using UnityEditor;
using UnityEngine;

public static class CrearEscenaPhysicsTestLab
{
    static readonly string[] nombresGeneradosAnteriores =
    {
        "Jugador", "Suelo", "ParedNorte", "ParedSur", "ParedEste", "ParedOeste",
        "Puerta", "BotonFisico", "Boton_Fisico", "Caja1", "Caja2", "Caja3",
        "Caja_Fisica_1", "Caja_Fisica_2", "Caja_Fisica_3", "Meta", "Bola_Lanzable",
        "Punto_Salida_Bola", "Zona_Fuerza", "Pared_Rompible"
    };

    [MenuItem("Tools/Physics Test Lab/Crear Escena Base")]
    public static void CrearEscenaBase()
    {
        LimpiarEscenaGenerada();

        GameObject raiz = new GameObject("PhysicsTestLab_Escena");

        Material matSuelo = ObtenerOCrearMaterial("Assets/Materials/Mat_Suelo.mat", new Color(0.55f, 0.55f, 0.55f));
        Material matBola = ObtenerOCrearMaterial("Assets/Materials/Mat_Bola_Azul.mat", Color.blue);
        Material matCaja = ObtenerOCrearMaterial("Assets/Materials/Mat_Caja_Gris.mat", new Color(0.65f, 0.65f, 0.65f));
        Material matBoton = ObtenerOCrearMaterial("Assets/Materials/Mat_Boton_Amarillo.mat", Color.yellow);
        Material matPuerta = ObtenerOCrearMaterial("Assets/Materials/Mat_Puerta_Oscura.mat", new Color(0.2f, 0.2f, 0.2f));
        Material matMeta = ObtenerOCrearMaterial("Assets/Materials/Mat_Meta_Verde.mat", Color.green);
        Material matFuerza = ObtenerOCrearMaterial("Assets/Materials/Mat_Zona_Fuerza.mat", new Color(0.35f, 0.8f, 1f, 0.6f));
        Material matRompible = ObtenerOCrearMaterial("Assets/Materials/Mat_Pared_Rompible.mat", new Color(1f, 0.3f, 0.3f, 0.75f));

        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Plane);
        suelo.name = "Suelo";
        suelo.transform.position = Vector3.zero;
        suelo.transform.localScale = new Vector3(2f, 1f, 2f);
        suelo.GetComponent<Renderer>().sharedMaterial = matSuelo;
        suelo.transform.SetParent(raiz.transform);

        CrearPared("ParedNorte", new Vector3(0f, 2f, 20f), new Vector3(40f, 4f, 1f), matCaja, raiz.transform);
        CrearPared("ParedSur", new Vector3(0f, 2f, -20f), new Vector3(40f, 4f, 1f), matCaja, raiz.transform);
        CrearPared("ParedEste", new Vector3(20f, 2f, 0f), new Vector3(1f, 4f, 40f), matCaja, raiz.transform);
        CrearPared("ParedOeste", new Vector3(-20f, 2f, 0f), new Vector3(1f, 4f, 40f), matCaja, raiz.transform);

        GameObject jugador = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        jugador.name = "Jugador";
        jugador.tag = "Player";
        jugador.transform.position = new Vector3(0f, 1f, -14f);
        jugador.transform.localScale = Vector3.one;
        jugador.AddComponent<MovimientoJugadorSimple>();
        Rigidbody rbJugador = jugador.AddComponent<Rigidbody>();
        rbJugador.constraints = RigidbodyConstraints.FreezeRotation;
        jugador.transform.SetParent(raiz.transform);

        GameObject puntoSalida = new GameObject("Punto_Salida_Bola");
        puntoSalida.transform.SetParent(jugador.transform);
        puntoSalida.transform.localPosition = new Vector3(0f, 1f, 1.2f);

        GameObject bolaPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bolaPrefab.name = "Bola_Lanzable";
        bolaPrefab.tag = "Bola";
        bolaPrefab.transform.position = new Vector3(0f, 2f, -12f);
        bolaPrefab.GetComponent<Renderer>().sharedMaterial = matBola;
        Rigidbody rbBola = bolaPrefab.AddComponent<Rigidbody>();
        rbBola.mass = 1f;
        bolaPrefab.SetActive(false);
        bolaPrefab.transform.SetParent(raiz.transform);

        LanzadorBola lanzador = jugador.AddComponent<LanzadorBola>();
        lanzador.prefabBola = bolaPrefab;
        lanzador.puntoSalida = puntoSalida.transform;

        for (int i = 0; i < 3; i++)
        {
            GameObject caja = GameObject.CreatePrimitive(PrimitiveType.Cube);
            caja.name = "Caja" + (i + 1);
            caja.tag = "Caja";
            caja.transform.position = new Vector3(-2f + (i * 2f), 0.8f, -2f);
            caja.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            caja.GetComponent<Renderer>().sharedMaterial = matCaja;
            caja.AddComponent<Rigidbody>();
            caja.transform.SetParent(raiz.transform);
        }

        GameObject boton = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boton.name = "BotonFisico";
        boton.transform.position = new Vector3(0f, 0.2f, 6f);
        boton.transform.localScale = new Vector3(2f, 0.3f, 2f);
        boton.GetComponent<Renderer>().sharedMaterial = matBoton;
        BoxCollider colBoton = boton.GetComponent<BoxCollider>();
        colBoton.isTrigger = true;
        boton.transform.SetParent(raiz.transform);

        GameObject puerta = GameObject.CreatePrimitive(PrimitiveType.Cube);
        puerta.name = "Puerta";
        puerta.transform.position = new Vector3(0f, 2f, 9f);
        puerta.transform.localScale = new Vector3(3f, 4f, 1f);
        puerta.GetComponent<Renderer>().sharedMaterial = matPuerta;
        puerta.transform.SetParent(raiz.transform);
        AbridorPuerta abridorPuerta = puerta.AddComponent<AbridorPuerta>();

        BotonFisico botonFisico = boton.AddComponent<BotonFisico>();
        botonFisico.puerta = abridorPuerta;

        GameObject meta = GameObject.CreatePrimitive(PrimitiveType.Cube);
        meta.name = "Meta";
        meta.transform.position = new Vector3(0f, 0.5f, 14f);
        meta.transform.localScale = new Vector3(4f, 1f, 2f);
        meta.GetComponent<Renderer>().sharedMaterial = matMeta;
        BoxCollider colMeta = meta.GetComponent<BoxCollider>();
        colMeta.isTrigger = true;
        meta.AddComponent<ZonaMeta>();
        meta.transform.SetParent(raiz.transform);

        GameObject zonaFuerza = GameObject.CreatePrimitive(PrimitiveType.Cube);
        zonaFuerza.name = "Zona_Fuerza";
        zonaFuerza.transform.position = new Vector3(-8f, 1f, -1f);
        zonaFuerza.transform.localScale = new Vector3(4f, 2f, 4f);
        zonaFuerza.GetComponent<Renderer>().sharedMaterial = matFuerza;
        zonaFuerza.GetComponent<BoxCollider>().isTrigger = true;
        zonaFuerza.AddComponent<ZonaFuerza>();
        zonaFuerza.transform.SetParent(raiz.transform);

        GameObject paredRompible = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paredRompible.name = "Pared_Rompible";
        paredRompible.transform.position = new Vector3(8f, 1.5f, 4f);
        paredRompible.transform.localScale = new Vector3(3f, 3f, 0.7f);
        paredRompible.GetComponent<Renderer>().sharedMaterial = matRompible;
        paredRompible.AddComponent<ParedRompiblePorVelocidad>();
        paredRompible.transform.SetParent(raiz.transform);

        Camera camara = Camera.main;
        if (camara == null)
        {
            GameObject objetoCamara = new GameObject("Main Camera");
            camara = objetoCamara.AddComponent<Camera>();
            objetoCamara.tag = "MainCamera";
        }
        camara.transform.position = new Vector3(0f, 12f, -22f);
        camara.transform.rotation = Quaternion.Euler(20f, 0f, 0f);

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

    static void LimpiarEscenaGenerada()
    {
        GameObject raizAnterior = GameObject.Find("PhysicsTestLab_Escena");
        if (raizAnterior != null)
        {
            Object.DestroyImmediate(raizAnterior);
        }

        for (int i = 0; i < nombresGeneradosAnteriores.Length; i++)
        {
            GameObject suelto = GameObject.Find(nombresGeneradosAnteriores[i]);
            if (suelto != null)
            {
                Object.DestroyImmediate(suelto);
            }
        }
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

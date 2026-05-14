using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CrearEscenaPhysicsTestLab
{
    [MenuItem("Tools/Physics Test Lab/Crear Escena Base")]
    public static void CrearEscenaBase()
    {
        GameObject anterior = GameObject.Find("PhysicsTestLab_Escena");
        if (anterior != null)
        {
            Object.DestroyImmediate(anterior);
        }

        GameObject raiz = new GameObject("PhysicsTestLab_Escena");
        raiz.transform.position = Vector3.zero;

        Material matSuelo = ObtenerOCrearMaterial("Mat_Suelo_Gris", new Color(0.55f, 0.55f, 0.55f));
        Material matPared = ObtenerOCrearMaterial("Mat_Pared_Oscura", new Color(0.22f, 0.22f, 0.22f));
        Material matBola = ObtenerOCrearMaterial("Mat_Bola_Azul", new Color(0.2f, 0.45f, 1f));
        Material matCaja = ObtenerOCrearMaterial("Mat_Caja_Gris", new Color(0.72f, 0.72f, 0.72f));
        Material matBoton = ObtenerOCrearMaterial("Mat_Boton_Amarillo", new Color(1f, 0.86f, 0.2f));
        Material matPuerta = ObtenerOCrearMaterial("Mat_Puerta_Oscura", new Color(0.15f, 0.15f, 0.18f));
        Material matMeta = ObtenerOCrearMaterial("Mat_Meta_Verde", new Color(0.2f, 1f, 0.25f));
        Material matParedRoja = ObtenerOCrearMaterial("Mat_Pared_Roja", new Color(1f, 0.2f, 0.2f));
        Material matZona = ObtenerOCrearMaterial("Mat_Zona_Fuerza_Azul", new Color(0.3f, 0.7f, 1f, 0.65f));
        Material matRampa = ObtenerOCrearMaterial("Mat_Rampa_Gris", new Color(0.63f, 0.63f, 0.63f));

        GameObject controlGO = new GameObject("Control_Victoria");
        controlGO.transform.SetParent(raiz.transform);
        ControlVictoria control = controlGO.AddComponent<ControlVictoria>();

        CrearEscenario(raiz.transform, matSuelo, matPared, matZona, matRampa, matParedRoja, matCaja, matBoton, matPuerta, matMeta, matBola, control);
        CrearJugador(raiz.transform, matBola);
        CrearCanvasVictoria(raiz.transform, control);
        CrearTextosAyuda(raiz.transform);

        Selection.activeGameObject = raiz;
        Debug.Log("Escena base de Physics Test Lab creada correctamente.");
    }

    static void CrearEscenario(Transform raiz, Material matSuelo, Material matPared, Material matZona, Material matRampa, Material matParedRoja, Material matCaja, Material matBoton, Material matPuerta, Material matMeta, Material matBola, ControlVictoria control)
    {
        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        suelo.name = "Suelo_Laboratorio";
        suelo.transform.position = new Vector3(0f, -0.5f, 35f);
        suelo.transform.localScale = new Vector3(14f, 1f, 90f);
        suelo.GetComponent<Renderer>().sharedMaterial = matSuelo;
        suelo.transform.SetParent(raiz);

        CrearCubo("Pared_Izquierda_1", new Vector3(-7f, 2f, 35f), new Vector3(1f, 4f, 90f), matPared, raiz);
        CrearCubo("Pared_Derecha_1", new Vector3(7f, 2f, 35f), new Vector3(1f, 4f, 90f), matPared, raiz);
        CrearCubo("Pared_Fondo", new Vector3(0f, 2f, -10f), new Vector3(14f, 4f, 1f), matPared, raiz);

        GameObject zonaLanzamiento = CrearCubo("Zona_Lanzamiento", new Vector3(0f, 0.03f, 8f), new Vector3(8f, 0.05f, 6f), matZona, raiz);
        zonaLanzamiento.GetComponent<Collider>().isTrigger = true;

        GameObject zonaFuerza = CrearCubo("Zona_Fuerza", new Vector3(0f, 1f, 18f), new Vector3(6f, 2f, 8f), matZona, raiz);
        zonaFuerza.GetComponent<BoxCollider>().isTrigger = true;
        ZonaFuerza zf = zonaFuerza.AddComponent<ZonaFuerza>();
        zf.direccionFuerza = new Vector3(0f, 0f, 40f);

        GameObject rampa = CrearCubo("Rampa_Fisica", new Vector3(0f, 1.1f, 25f), new Vector3(4f, 0.7f, 7f), matRampa, raiz);
        rampa.transform.rotation = Quaternion.Euler(25f, 0f, 0f);

        GameObject pared = CrearCubo("Pared_Rompible", new Vector3(0f, 2f, 32f), new Vector3(6f, 4f, 1f), matParedRoja, raiz);
        ParedRompiblePorVelocidad rompible = pared.AddComponent<ParedRompiblePorVelocidad>();
        rompible.controlVictoria = control;

        // Estas cajas son objetos físicos interactivos
        CrearCajaFisica("Caja_Fisica_1", new Vector3(-2f, 0.8f, 40f), matCaja, raiz);
        CrearCajaFisica("Caja_Fisica_2", new Vector3(2f, 0.8f, 43f), matCaja, raiz);
        CrearCajaFisica("Caja_Fisica_3", new Vector3(-1f, 0.8f, 46f), matCaja, raiz);
        CrearCajaFisica("Caja_Fisica_4", new Vector3(1f, 0.8f, 49f), matCaja, raiz);

        GameObject boton = CrearCubo("Boton_Fisico", new Vector3(0f, 0.2f, 53f), new Vector3(3f, 0.4f, 3f), matBoton, raiz);
        boton.GetComponent<BoxCollider>().isTrigger = true;

        GameObject puerta = CrearCubo("Puerta_Principal", new Vector3(0f, 2.5f, 58f), new Vector3(6f, 5f, 1f), matPuerta, raiz);
        AbridorPuerta abridor = puerta.AddComponent<AbridorPuerta>();
        abridor.controlVictoria = control;

        BotonFisico botonScript = boton.AddComponent<BotonFisico>();
        botonScript.puerta = abridor;
        botonScript.controlVictoria = control;
        botonScript.renderBoton = boton.GetComponent<Renderer>();

        CrearCuboMovil("Obstaculo_1", new Vector3(-1.5f, 0.8f, 68f), new Vector3(2f, 1.6f, 2f), matCaja, raiz);
        CrearCuboMovil("Obstaculo_2", new Vector3(1.8f, 0.8f, 74f), new Vector3(1.8f, 1.8f, 1.8f), matCaja, raiz);
        CrearEsferaMovil("Obstaculo_3", new Vector3(0f, 1f, 80f), 1.6f, matBola, raiz);

        GameObject meta = CrearCubo("Meta_Final", new Vector3(0f, 0.6f, 86f), new Vector3(6f, 1.2f, 3f), matMeta, raiz);
        meta.GetComponent<BoxCollider>().isTrigger = true;
        ZonaMeta zonaMeta = meta.AddComponent<ZonaMeta>();
        zonaMeta.controlVictoria = control;
    }

    static void CrearJugador(Transform raiz, Material matBola)
    {
        GameObject jugador = GameObject.Find("Jugador");
        if (jugador == null)
        {
            jugador = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            jugador.name = "Jugador";
        }

        jugador.tag = "Player";
        jugador.transform.position = new Vector3(0f, 1f, 2f);
        jugador.transform.localScale = Vector3.one;
        jugador.transform.SetParent(raiz);

        if (jugador.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = jugador.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (jugador.GetComponent<MovimientoJugadorSimple>() == null)
        {
            jugador.AddComponent<MovimientoJugadorSimple>();
        }

        Transform punto = jugador.transform.Find("Punto_Salida_Bola");
        if (punto == null)
        {
            GameObject puntoGO = new GameObject("Punto_Salida_Bola");
            puntoGO.transform.SetParent(jugador.transform);
            punto = puntoGO.transform;
        }
        punto.localPosition = new Vector3(0f, 1.1f, 1.2f);

        GameObject prefabBola = GameObject.Find("Bola_Lanzable");
        if (prefabBola == null)
        {
            prefabBola = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            prefabBola.name = "Bola_Lanzable";
        }
        prefabBola.transform.SetParent(raiz);
        prefabBola.transform.position = new Vector3(0f, 1f, 6f);
        prefabBola.GetComponent<Renderer>().sharedMaterial = matBola;
        prefabBola.tag = "Bola";
        if (prefabBola.GetComponent<Rigidbody>() == null)
        {
            prefabBola.AddComponent<Rigidbody>();
        }
        prefabBola.SetActive(false);

        LanzadorBola lanzador = jugador.GetComponent<LanzadorBola>();
        if (lanzador == null)
        {
            lanzador = jugador.AddComponent<LanzadorBola>();
        }
        lanzador.prefabBola = prefabBola;
        lanzador.puntoSalida = punto;

        Camera cam = Camera.main;
        if (cam == null)
        {
            GameObject camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            cam = camGO.AddComponent<Camera>();
        }
        cam.transform.SetParent(raiz);
        CamaraSeguimientoSimple seguimiento = cam.GetComponent<CamaraSeguimientoSimple>();
        if (seguimiento == null)
        {
            seguimiento = cam.gameObject.AddComponent<CamaraSeguimientoSimple>();
        }
        seguimiento.objetivo = jugador.transform;
        seguimiento.desplazamiento = new Vector3(0f, 9f, -11f);
    }

    static void CrearCanvasVictoria(Transform raiz, ControlVictoria control)
    {
        GameObject canvasGO = new GameObject("Canvas_Victoria");
        canvasGO.transform.SetParent(raiz);
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject panel = new GameObject("Panel_Victoria");
        panel.transform.SetParent(canvasGO.transform, false);
        Image img = panel.AddComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 0.65f);
        RectTransform rtPanel = panel.GetComponent<RectTransform>();
        rtPanel.anchorMin = Vector2.zero; rtPanel.anchorMax = Vector2.one; rtPanel.offsetMin = Vector2.zero; rtPanel.offsetMax = Vector2.zero;

        CrearTextoUI(panel.transform, "Texto_Ganaste", "HAS GANADO", 56, new Vector2(0f, 60f));
        CrearTextoUI(panel.transform, "Texto_Detalle", "Has completado todas las pruebas de física", 30, new Vector2(0f, -10f));

        GameObject avisoGO = CrearTextoUI(canvasGO.transform, "Texto_Aviso", "", 26, new Vector2(0f, -220f));
        control.panelVictoria = panel;
        control.textoAviso = avisoGO.GetComponent<Text>();
    }

    static GameObject CrearTextoUI(Transform padre, string nombre, string contenido, int tam, Vector2 pos)
    {
        GameObject go = new GameObject(nombre);
        go.transform.SetParent(padre, false);
        Text txt = go.AddComponent<Text>();
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.text = contenido;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.fontSize = tam;
        txt.color = Color.white;
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1200f, 120f);
        rt.anchoredPosition = pos;
        return go;
    }

    static void CrearTextosAyuda(Transform padre)
    {
        CrearTextoMundo("1. Lanza la bola con E", new Vector3(0f, 2.6f, 7f), padre);
        CrearTextoMundo("2. Usa la zona azul para ganar velocidad", new Vector3(0f, 2.6f, 18f), padre);
        CrearTextoMundo("3. Rompe la pared roja con la bola", new Vector3(0f, 3.4f, 31f), padre);
        CrearTextoMundo("4. Empuja una caja al botón amarillo", new Vector3(0f, 2.6f, 51f), padre);
        CrearTextoMundo("5. Cruza la puerta", new Vector3(0f, 3f, 58f), padre);
        CrearTextoMundo("6. Llega a la meta verde", new Vector3(0f, 2.8f, 85f), padre);
    }

    static void CrearTextoMundo(string mensaje, Vector3 pos, Transform padre)
    {
        GameObject go = new GameObject("Texto_" + mensaje.Replace(" ", "_"));
        go.transform.SetParent(padre);
        go.transform.position = pos;
        TextMesh tm = go.AddComponent<TextMesh>();
        tm.text = mensaje;
        tm.fontSize = 42;
        tm.characterSize = 0.12f;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.color = Color.white;
    }

    static GameObject CrearCajaFisica(string nombre, Vector3 pos, Material mat, Transform padre)
    {
        GameObject caja = CrearCubo(nombre, pos, new Vector3(1.4f, 1.4f, 1.4f), mat, padre);
        caja.tag = "Caja";
        caja.AddComponent<Rigidbody>(); // Requisito: objetos físicos interactivos con Rigidbody
        return caja;
    }

    static GameObject CrearCuboMovil(string nombre, Vector3 pos, Vector3 escala, Material mat, Transform padre)
    {
        GameObject c = CrearCubo(nombre, pos, escala, mat, padre);
        c.AddComponent<Rigidbody>();
        return c;
    }

    static GameObject CrearEsferaMovil(string nombre, Vector3 pos, float escala, Material mat, Transform padre)
    {
        GameObject esfera = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        esfera.name = nombre;
        esfera.transform.SetParent(padre);
        esfera.transform.position = pos;
        esfera.transform.localScale = Vector3.one * escala;
        esfera.GetComponent<Renderer>().sharedMaterial = mat;
        esfera.AddComponent<Rigidbody>();
        return esfera;
    }

    static GameObject CrearCubo(string nombre, Vector3 pos, Vector3 escala, Material mat, Transform padre)
    {
        GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
        c.name = nombre;
        c.transform.SetParent(padre);
        c.transform.position = pos;
        c.transform.localScale = escala;
        c.GetComponent<Renderer>().sharedMaterial = mat;
        return c;
    }

    static Material ObtenerOCrearMaterial(string nombre, Color color)
    {
        if (!AssetDatabase.IsValidFolder("Assets/Materials"))
        {
            AssetDatabase.CreateFolder("Assets", "Materials");
        }

        string ruta = "Assets/Materials/" + nombre + ".mat";
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(ruta);
        if (mat == null)
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null) shader = Shader.Find("Standard");
            mat = new Material(shader);
            AssetDatabase.CreateAsset(mat, ruta);
        }
        mat.color = color;
        EditorUtility.SetDirty(mat);
        AssetDatabase.SaveAssets();
        return mat;
    }
}

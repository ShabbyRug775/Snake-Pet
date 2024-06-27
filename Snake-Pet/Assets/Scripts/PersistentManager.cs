using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PersistentManager : MonoBehaviour
{
    private static PersistentManager instance;

    // Lista de nombres de escenas donde el objeto no debe persistir
    private List<string> excludedScenes = new List<string>
    {
        "crear_cuenta", "Menu_Principal", "seleccion_cuenta"
    };

    // Diccionario para almacenar posiciones específicas para ciertas escenas
    private Dictionary<string, Vector2> scenePositions = new Dictionary<string, Vector2>
    {
        { "per_mascota", new Vector2(812, 250) },
        { "tienda", new Vector2(812, 250) }
    };

    // Lista de nombres de escenas donde el objeto debe estar presente pero no visible
    private List<string> invisibleScenes = new List<string>
    {
        //"ver_tareas", "tareas_hechas", "crear_tarea"
    };

    private SpriteRenderer spriteRenderer;
    private Renderer[] renderers;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Obtener todos los componentes Renderer del objeto
        renderers = GetComponentsInChildren<Renderer>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si la escena actual está en la lista de escenas excluidas, destruye el objeto
        if (excludedScenes.Contains(scene.name))
        {
            Destroy(gameObject);
            instance = null; // Resetear la instancia para permitir que se vuelva a crear en escenas no excluidas
        }
        else
        {
            // Cambiar la posición del objeto si la escena está en el diccionario
            if (scenePositions.ContainsKey(scene.name))
            {
                transform.position = scenePositions[scene.name];
            }

            // Ocultar el objeto si la escena está en la lista de escenas invisibles
            bool shouldHide = invisibleScenes.Contains(scene.name);
            foreach (var renderer in renderers)
            {
                renderer.enabled = !shouldHide;
            }
        }
    }
}

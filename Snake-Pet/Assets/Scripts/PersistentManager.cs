using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PersistentManager : MonoBehaviour
{
    private static PersistentManager instance;

    // Lista de nombres de escenas donde el objeto no debe persistir
    private List<string> excludedScenes = new List<string>
    {
        "crear_cuenta", "crear_tarea", "Menu_Principal", "seleccion_cuenta", "tareas_hechas", "ver_tareas"
    };

    // Diccionario para almacenar posiciones específicas para ciertas escenas
    private Dictionary<string, Vector2> scenePositions = new Dictionary<string, Vector2>
    {
        { "per_mascota", new Vector2(150, 0) },
        { "tienda", new Vector2(150, 0) },
        { "mascota", new Vector2(-90, 0) }
    };

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
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
        else if (scenePositions.ContainsKey(scene.name))
        {
            // Cambiar la posición del objeto si la escena está en el diccionario
            transform.position = scenePositions[scene.name];
        }
    }
}

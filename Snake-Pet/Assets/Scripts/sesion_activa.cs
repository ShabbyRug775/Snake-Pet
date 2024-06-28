using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class sesion_activa : MonoBehaviour
{
    private static sesion_activa instance;

    // Lista de nombres de escenas donde el objeto no debe persistir
    private List<string> excludedScenes = new List<string>
    {
        "crear_cuenta", "Menu_Principal", "seleccion_cuenta"
    };

    void Awake()
    {
        if (instance != null && instance != this)
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
        else
        {
            // Si no está en la lista de excluidas, asegurarse de que el objeto esté activo
            gameObject.SetActive(true);
        }
    }
}

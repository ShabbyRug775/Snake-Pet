using UnityEngine;

public class SnakeAddSingleton : MonoBehaviour
{
    public static SnakeAddSingleton Instance;

    public Snake_add snake;

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia del Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Ya hay otra instancia, destruir esta
        }
    }
}

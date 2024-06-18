using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiar_escena : MonoBehaviour
{
    // Se cambia la escena
    public void cambiarEscena(string nombre)
    {
        // Carga la siguiente escena
        SceneManager.LoadScene(nombre);
    }
}

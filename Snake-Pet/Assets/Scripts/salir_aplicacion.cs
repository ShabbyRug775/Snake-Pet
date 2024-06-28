using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salir_aplicacion : MonoBehaviour
{
    // Se sale de la aplicación
    public void ExitApplication()
    {
        Application.Quit();
        
        // Para salir del modo juego
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
    }
}

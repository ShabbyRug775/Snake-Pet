using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MostrarDatos : MonoBehaviour
{
    public Transform usuariosParent;  // Referencia al padre donde se crearán los objetos para cada usuario
    public GameObject partidaGuardadaPrefab;  // Prefab para mostrar la información de cada usuario
    private string filePath;
    public float verticalSpacing = 50f;

    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Scripts/snake.json");
        MostrarInformacion();
    }

    private void MostrarInformacion()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            UsuariosData usuariosData = JsonUtility.FromJson<UsuariosData>(jsonData);

            for (int i = 0; i < usuariosData.usuarios.Count; i++)
            {
                Usuario usuario = usuariosData.usuarios[i];
                GameObject usuarioObj = Instantiate(partidaGuardadaPrefab, usuariosParent);
                usuarioObj.transform.SetParent(usuariosParent, false);  // Asegurarse de que se mantiene la configuración de UI

                // Ajustar la posición del objeto
                RectTransform rt = usuarioObj.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -i * verticalSpacing);
                }

                ControladorMostrarDatos controlador = usuarioObj.GetComponent<ControladorMostrarDatos>();
                controlador.AsignarDatos(usuario);
            }
        }
        else
        {
            Debug.Log("No se encontró el archivo JSON.");
        }
    }
}

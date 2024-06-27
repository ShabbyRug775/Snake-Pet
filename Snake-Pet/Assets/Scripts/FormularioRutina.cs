using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class FormularioRutina : MonoBehaviour
{
    public TMP_InputField nombreRutinaInput;
    public TMP_Text nombreUsuarioText;
    public Button guardarRutinaButton;
    private string filePath;

    private void Start()
    {
        // Establecer la ruta del archivo JSON en la carpeta de Assets
        filePath = Path.Combine(Application.dataPath, "Scripts/snake.json");

        // Verificar si los InputFields están asignados correctamente
        if (nombreRutinaInput == null || nombreUsuarioText == null)
        {
            Debug.LogError("Los campos de entrada no están asignados correctamente en el Inspector.");
            return;
        }

        guardarRutinaButton.onClick.AddListener(GuardarRutina);
    }

    public void Update()
    {
        // Validar si los campos de nombre están vacíos
        if (string.IsNullOrEmpty(nombreRutinaInput.text) || string.IsNullOrEmpty(nombreUsuarioText.text))
        {
            // Si alguno de los campos está vacío, el botón se desactiva
            guardarRutinaButton.interactable = false;
        }
        else
        {
            // Si ambos campos tienen algo, el botón se activa
            guardarRutinaButton.interactable = true;
        }
    }

    private void GuardarRutina()
    {
        string nombreRutina = nombreRutinaInput.text;
        string nombreUsuario = nombreUsuarioText.text;

        // Cargar los datos actuales de usuarios
        UsuariosData usuariosData = CargarUsuarios();

        // Buscar el usuario correspondiente
        Usuario usuario = usuariosData.usuarios.Find(u => u.Nombre == nombreUsuario);
        if (usuario == null)
        {
            Debug.LogError("Usuario no encontrado: " + nombreUsuario);
            return;
        }

        // Verificar si ya existe una rutina con el mismo nombre
        bool rutinaExistente = usuario.Rutinas.Exists(rutina => rutina.Nombre_Rutina == nombreRutina);
        if (rutinaExistente)
        {
            guardarRutinaButton.interactable = false;
            return;
        }

        Rutina nuevaRutina = new Rutina
        {
            ID_Rutina = Guid.NewGuid().ToString(),
            Nombre_Rutina = nombreRutina,
            Fecha_Rutina = DateTime.Now.ToString("yyyy-MM-dd"),
            Porcentaje_Rutina = "0"
        };

        usuario.Rutinas.Add(nuevaRutina);

        // Convertir a JSON y guardar en el archivo
        string jsonData = JsonUtility.ToJson(usuariosData, true);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Rutina guardada en: " + filePath);
    }

    private UsuariosData CargarUsuarios()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<UsuariosData>(jsonData);
        }
        else
        {
            return new UsuariosData();
        }
    }
}

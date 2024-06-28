using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class Accesorio
{
    public string ID_Accesorio;
    public string Nombre_Accesorio;
    public string Fecha_Obtencion;
}

[Serializable]
public class Mascota
{
    public string ID_Mascota;
    public string Fecha_Creacion;
    public List<Accesorio> Accesorios;
}

[Serializable]
public class Rutina
{
    public string ID_Rutina;
    public string Nombre_Rutina;
    public string Porcentaje_Rutina;
    public string Fecha_Rutina;
}

[Serializable]
public class Usuario
{
    public string ID_Usuario;
    public string Nombre;
    public List<Rutina> Rutinas;
    public List<Mascota> Mascotas;
}

[Serializable]
public class UsuariosData
{
    public List<Usuario> usuarios = new List<Usuario>();
}

public class formulario : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public Button guardarButton;
    private string filePath;

    private void Start()
    {
        // Establecer la ruta del archivo JSON en la carpeta de Assets
        filePath = Path.Combine(Application.dataPath, "Scripts/snake.json");

        // Verificar si el archivo JSON existe, si no existe, crearlo con datos vacíos
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(new UsuariosData()));
        }

        // Verificar si los InputFields están asignados correctamente
        if (nombreInput == null)
        {
            Debug.LogError("Los campos de entrada no están asignados correctamente en el Inspector.");
            return;
        }

        guardarButton.onClick.AddListener(GuardarUsuario);
    }


    public void Update()
    {
        // Validar si el nombreInput está vacío
        if (string.IsNullOrEmpty(nombreInput.text))
        {
            // Si el campo de nombre está vacío, el botón se desactiva
            guardarButton.interactable = false;
        }
        else
        {
            // Si el campo de nombre tiene algo, el botón se activa
            guardarButton.interactable = true;
        }
    }

    private void GuardarUsuario()
    {
        string nombre = nombreInput.text;

        // Cargar los datos actuales de usuarios
        UsuariosData usuariosData = CargarUsuarios();

        // Verificar si el nombre de usuario ya existe
        if (usuariosData.usuarios.Exists(u => u.Nombre == nombre))
        {
            guardarButton.interactable = false;

            return;
        }

        Usuario nuevoUsuario = new Usuario
        {
            ID_Usuario = Guid.NewGuid().ToString(),
            Nombre = nombre,
            Rutinas = new List<Rutina>(),
            Mascotas = new List<Mascota>()
        };

        usuariosData.usuarios.Add(nuevoUsuario);

        // Convertir a JSON y guardar en el archivo
        string jsonData = JsonUtility.ToJson(usuariosData, true);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Usuario guardado en: " + filePath);
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

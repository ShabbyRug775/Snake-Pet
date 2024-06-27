using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class MostrarRutinas : MonoBehaviour
{
    public Transform rutinasParent;  // Referencia al padre donde se crearán los objetos para cada rutina
    public GameObject rutinaPrefab;  // Prefab para mostrar la información de cada rutina
    private string filePath;
    public float verticalSpacing = 50f;  // Espaciado vertical entre rutinas

    // Referencia al TMP_Text que contiene el nombre del usuario
    public TMP_Text nombreUsuarioText;

    private List<Rutina> currentRutinas = new List<Rutina>();

    private void Start()
    {
        // Establecer la ruta del archivo JSON en la carpeta de Assets
        filePath = Path.Combine(Application.dataPath, "Scripts/snake.json");

        // Verificar si los InputFields están asignados correctamente
        if (nombreUsuarioText == null)
        {
            Debug.LogError("Los campos de entrada no están asignados correctamente en el Inspector.");
            return;
        }

        ConsultasRutinas();
    }

    private void ConsultasRutinas()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            UsuariosData usuariosData = JsonUtility.FromJson<UsuariosData>(jsonData);

            // Obtener el nombre de usuario desde TMP_Text
            string nombreUsuario = nombreUsuarioText.text;

            // Buscar el usuario en los datos cargados
            Usuario usuario = usuariosData.usuarios.Find(u => u.Nombre == nombreUsuario);
            if (usuario != null)
            {
                currentRutinas = usuario.Rutinas;
                // Mostrar las rutinas del usuario encontrado
                foreach (var rutina in usuario.Rutinas)
                {
                    // Verificar si Porcentaje_Rutina es igual a "100"
                    if (rutina.Porcentaje_Rutina == "100")
                    {
                        // Si es 100, no hacemos nada y pasamos a la siguiente rutina
                        continue;
                    }

                    // Crear objeto para mostrar la rutina
                    GameObject rutinaObj = Instantiate(rutinaPrefab, rutinasParent);
                    // Ajustar la posición del objeto de la rutina
                    RectTransform rtRutina = rutinaObj.GetComponent<RectTransform>();
                    if (rtRutina != null)
                    {
                        rtRutina.anchoredPosition = new Vector2(rtRutina.anchoredPosition.x, -rutinasParent.childCount * verticalSpacing);
                    }

                    // Asignar el nombre de la rutina (por ejemplo, a un TextMeshPro)
                    TMP_Text nombreRutinaText = rutinaObj.GetComponentInChildren<TMP_Text>();
                    if (nombreRutinaText != null)
                    {
                        nombreRutinaText.text = rutina.Nombre_Rutina;
                    }

                    // Encontrar los botones dentro del prefab y añadirles listeners
                    Button completarButton = rutinaObj.transform.Find("CompletarButton").GetComponent<Button>();
                    Button eliminarButton = rutinaObj.transform.Find("EliminarButton").GetComponent<Button>();

                    if (completarButton != null)
                    {
                        completarButton.onClick.AddListener(() => CompletarRutina(rutina, rutinaObj));
                    }

                    if (eliminarButton != null)
                    {
                        eliminarButton.onClick.AddListener(() => EliminarRutina(rutina, rutinaObj));
                    }
                }
            }
            else
            {
                Debug.LogError("Usuario no encontrado: " + nombreUsuario);
            }
        }
        else
        {
            Debug.Log("No se encontró el archivo JSON.");
        }
    }

    private void CompletarRutina(Rutina rutina, GameObject rutinaObj)
    {
        rutina.Porcentaje_Rutina = "100";
        SaveJsonChanges();
        Destroy(rutinaObj);
    }

    private void EliminarRutina(Rutina rutina, GameObject rutinaObj)
    {
        currentRutinas.Remove(rutina);
        SaveJsonChanges();
        Destroy(rutinaObj);
    }

    private void SaveJsonChanges()
    {
        string jsonData = File.ReadAllText(filePath);
        UsuariosData usuariosData = JsonUtility.FromJson<UsuariosData>(jsonData);

        // Obtener el nombre de usuario desde TMP_Text
        string nombreUsuario = nombreUsuarioText.text;
        Usuario usuario = usuariosData.usuarios.Find(u => u.Nombre == nombreUsuario);

        if (usuario != null)
        {
            usuario.Rutinas = currentRutinas;
            jsonData = JsonUtility.ToJson(usuariosData, true);
            File.WriteAllText(filePath, jsonData);
        }
    }
}

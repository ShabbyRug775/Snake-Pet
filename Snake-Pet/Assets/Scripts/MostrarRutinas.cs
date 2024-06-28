using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class MostrarRutinas : MonoBehaviour
{
    public Transform rutinasParent;
    public GameObject rutinaPrefab;
    private string filePath;
    public float verticalSpacing = 50f;
    public TMP_Text nombreUsuarioText;

    private List<Rutina> currentRutinas = new List<Rutina>();

    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Scripts/snake.json");

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

            string nombreUsuario = nombreUsuarioText.text;

            Usuario usuario = usuariosData.usuarios.Find(u => u.Nombre == nombreUsuario);
            if (usuario != null)
            {
                currentRutinas = usuario.Rutinas;
                foreach (var rutina in usuario.Rutinas)
                {
                    if (rutina.Porcentaje_Rutina == "100")
                    {
                        continue;
                    }

                    GameObject rutinaObj = Instantiate(rutinaPrefab, rutinasParent);
                    RectTransform rtRutina = rutinaObj.GetComponent<RectTransform>();
                    if (rtRutina != null)
                    {
                        rtRutina.anchoredPosition = new Vector2(rtRutina.anchoredPosition.x, -rutinasParent.childCount * verticalSpacing);
                    }

                    TMP_Text nombreRutinaText = rutinaObj.GetComponentInChildren<TMP_Text>();
                    if (nombreRutinaText != null)
                    {
                        nombreRutinaText.text = rutina.Nombre_Rutina;
                    }

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

        // Acceder al Singleton de SnakeAddSingleton para obtener la referencia de snake
        Snake_add snake = SnakeAddSingleton.Instance.snake;
        if (snake != null)
        {
            snake.AddBodyPart();
        }
        else
        {
            Debug.LogError("Referencia a Snake_add no encontrada.");
        }
    }

    private void EliminarRutina(Rutina rutina, GameObject rutinaObj)
    {
        currentRutinas.Remove(rutina);
        SaveJsonChanges();
        Destroy(rutinaObj);

        // Acceder al Singleton de SnakeAddSingleton para obtener la referencia de snake
        Snake_add snake = SnakeAddSingleton.Instance.snake;
        if (snake != null)
        {
            snake.RemoveAllBodyParts();
        }
        else
        {
            Debug.LogError("Referencia a Snake_add no encontrada.");
        }

    }

    private void SaveJsonChanges()
    {
        string jsonData = File.ReadAllText(filePath);
        UsuariosData usuariosData = JsonUtility.FromJson<UsuariosData>(jsonData);

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

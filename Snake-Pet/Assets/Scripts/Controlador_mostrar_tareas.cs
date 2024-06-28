using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controlador_mostrar_tareas : MonoBehaviour
{
    public TMP_Text nombreUsuarioText;

    private void Start()
    {

    }

    public void AsignarDatos(Usuario usuario)
    {
        nombreUsuarioText.text = usuario.Nombre;
    }

    public void SeleccionarUsuarioYIrAEscena()
    {
        UsuarioSeleccionado.Nombre = nombreUsuarioText.text;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMostrarDatos : MonoBehaviour
{
    public TMP_Text nombreUsuarioText;
    public Button selectButton; // Asegúrate de asignar este botón en el inspector

    private void Start()
    {
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(SeleccionarUsuarioYIrAEscena);
        }
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

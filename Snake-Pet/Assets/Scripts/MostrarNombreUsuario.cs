using TMPro;
using UnityEngine;

public class MostrarNombreUsuario : MonoBehaviour
{
    public TMP_Text nombreUsuarioText;

    private void Start()
    {
        if (nombreUsuarioText != null)
        {
            nombreUsuarioText.text = UsuarioSeleccionado.Nombre;
        }
    }
}

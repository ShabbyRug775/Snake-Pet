using TMPro;
using UnityEngine;

public class ControladorMostrarDatos : MonoBehaviour
{
    public TMP_Text nombreUsuarioText;

    public void AsignarDatos(Usuario usuario)
    {
        nombreUsuarioText.text = usuario.Nombre;
    }
}

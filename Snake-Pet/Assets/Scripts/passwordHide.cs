using UnityEngine;
using UnityEngine.UI;

public class PasswordHide : MonoBehaviour
{
    public InputField inputFieldToHide; // Este campo se asigna en el Inspector

    private string hiddenText = "";

    void Start()
    {
        if (inputFieldToHide != null)
        {
            inputFieldToHide.contentType = InputField.ContentType.Standard; // Configura el tipo de contenido como Standard o Alphanumeric
            inputFieldToHide.inputType = InputField.InputType.Password; // Configura el tipo de entrada como Password

            inputFieldToHide.onValueChanged.AddListener(UpdateHiddenText);
        }
        else
        {
            Debug.LogError("No se asignó ningún InputField al campo inputFieldToHide en el Inspector.");
        }
    }

    void UpdateHiddenText(string visibleText)
    {
        hiddenText = "";
        for (int i = 0; i < visibleText.Length; i++)
        {
            hiddenText += "*"; // Cambia '*' por cualquier otro carácter deseado (por ejemplo, '.')
        }
        inputFieldToHide.text = hiddenText;
        inputFieldToHide.caretPosition = hiddenText.Length; // Mueve el cursor al final del texto oculto
    }
}

using UnityEngine;
using TMPro;

public class DateFormatter : MonoBehaviour
{
    public TMP_InputField inputField;
    private bool isUpdating = false;

    void Start()
    {
        inputField.onValueChanged.AddListener(FormatInput);
    }

    void FormatInput(string input)
    {
        if (isUpdating) return;

        isUpdating = true;

        // Remove all non-digit characters
        string digitsOnly = System.Text.RegularExpressions.Regex.Replace(input, @"\D", "");

        // Format the input as MM/DD/YYYY
        if (digitsOnly.Length > 2)
        {
            digitsOnly = digitsOnly.Insert(2, "/");
        }
        if (digitsOnly.Length > 5)
        {
            digitsOnly = digitsOnly.Insert(5, "/");
        }

        // Limit the length to 10 characters (MM/DD/YYYY)
        if (digitsOnly.Length > 10)
        {
            digitsOnly = digitsOnly.Substring(0, 10);
        }

        inputField.text = digitsOnly;
        isUpdating = false;
    }
    void Update()
    {
        // Check if the caret is not at the end of the text
        if (inputField.caretPosition != inputField.text.Length)
        {
            // Move the caret to the end of the text
            inputField.caretPosition = inputField.text.Length;
        }
    }

}

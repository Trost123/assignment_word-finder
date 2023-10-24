using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI letterText;
    
    public void SetLetter(string letter)
    {
        if (letter == "_")
        {
            letter = "";
        }
        
        letterText.text = letter;
    }
}
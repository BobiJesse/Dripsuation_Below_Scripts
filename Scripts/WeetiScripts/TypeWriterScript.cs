using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriterScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private float typingSpeed = 0.05f;

    void Start()
    {
        StartCoroutine(TypeText("Meanwhile at the SUBer secret submarine . . . . ."));
    }

    IEnumerator TypeText(string message)
    {
        textComponent.text = message;
        textComponent.maxVisibleCharacters = 0;

        for (int i = 0; i <= message.Length; i++)
        {
            textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI bucketAmount;
    public TextMeshProUGUI Score;
    public Image oxygenLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bucketAmount.text = Dropletcatcher.instance.currentHoldAmount + " / 3";
        Score.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        oxygenLevel.fillAmount = Mathf.Clamp(PlayerMovement.instance.oxygenLevel, 0, 1);
    }
}

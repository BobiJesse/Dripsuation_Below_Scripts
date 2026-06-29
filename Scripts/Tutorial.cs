using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;
        tutorial1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next1()
    {
        tutorial1.SetActive (false);
        tutorial2 .SetActive (true);
    }

    public void Next2()
    {
        tutorial2.SetActive (false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; //lock the cursor
        Cursor.visible = false; //hide the cursor
    }
}

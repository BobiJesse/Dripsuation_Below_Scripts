using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] float WaitTime;
    [SerializeField] string SceneToLoad;


    private void Start()
    {
        StartCoroutine(SceneLoader());
    }

    IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(SceneToLoad);
    }
}

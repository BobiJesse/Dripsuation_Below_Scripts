using System.Collections;
using UnityEngine;

public class TorpedoLaunchScript : MonoBehaviour
{
    [SerializeField] GameObject AnimatedLauncher;
    [SerializeField] GameObject StaticLauncher;

    //Mittari
    [SerializeField] WaterMeterScript WaterMeterScript;

    [SerializeField] float WaitTime;
    public bool launchActive;

    public void ActivateLaunch()
    {
        if (!launchActive)
        {
            StartCoroutine(StartLaunch());
        }
    }

    IEnumerator StartLaunch()
    {
        launchActive = true;
        GameManager.instance.AddScore(6);
        UIManager.instance.Score.text = GameManager.instance.currentScore.ToString();
        AnimatedLauncher.SetActive(true);
        StaticLauncher.SetActive(false);
        yield return new WaitForSeconds(WaitTime);
        AnimatedLauncher.SetActive(false);
        StaticLauncher.SetActive(true);
        launchActive = false;

        //Mittarin tyhjennys
        WaterMeterScript.EmptyTank();
    }

}

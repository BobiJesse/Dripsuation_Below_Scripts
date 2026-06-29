using UnityEngine;

public class WaterMeterScript : MonoBehaviour
{
    [SerializeField] GameObject[] Waters;

    public void ChangeWaterLevel(int WaterLevel)
    {
        switch (WaterLevel)
        {
            case 1:
                {
                    Waters[0].SetActive(true);
                    break;
                }
            case 2:
                {
                    Waters[0].SetActive(true);
                    Waters[1].SetActive(true);
                    break;
                }
            case 3:
                {
                    Waters[0].SetActive(true);
                    Waters[1].SetActive(true);
                    Waters[2].SetActive(true);
                    break;
                }
        }
    }
    
    public void EmptyTank()
    {
        for (int i = 0; i < Waters.Length; i++)
        {
            Waters[i].SetActive(false);
        }
    }
}

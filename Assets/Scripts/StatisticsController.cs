using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class StatisticsController : MonoBehaviour
{
    public TextMeshProUGUI textMaxDist;
    public TextMeshProUGUI textMaxScore;

    void Start()
    {
        // ResetStatistics();
        GetMaxDistance();
        GetMaxScore();
    }

    public void GetMaxDistance()
    {
        textMaxDist.text = PlayerPrefs.GetFloat("MaxPlayerDistance", 0).ToString();
    }

    public void GetMaxScore()
    {
        textMaxScore.text = PlayerPrefs.GetFloat("MaxPlayerScore", 0).ToString();
    }

    private void ResetStatistics()
    {
        PlayerPrefs.SetFloat("MaxPlayerDistance", 0);
        PlayerPrefs.SetFloat("MaxPlayerScore", 0);
        PlayerPrefs.Save();
    }
}

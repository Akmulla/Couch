using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    public static Ads ads;
    [SerializeField]
    static int gamesToShowAds=8;
    static int gameCount;

    void Awake()
    {
        ads = this;
    }

    void Start()
    {
        gameCount = 0;
    }

    public void Increase()
    {
        gameCount++;
        if (gameCount >= gamesToShowAds)
        {
            StartCoroutine(ShowAds());
            gameCount = 0;
        }
    }

    IEnumerator ShowAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            yield break;
        }
        // Ads are not initialized yet, wait a little and try again
        yield return new WaitForSeconds(1f);

        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            yield break;
        }

        Debug.LogError("Something wrong");
    }
}

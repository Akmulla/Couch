using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;


public class UI1 : MonoBehaviour
{

    void Start()
    {
        // We use coroutine and not calling Show() directly because
        // it is possible that at this point ads are not initialized yet
        StartCoroutine(ShowAds());
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

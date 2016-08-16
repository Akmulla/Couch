using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAdsButton : MonoBehaviour
{
    [SerializeField]
    private string placementId = null;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();

        if (_button)
        {
            _button.interactable = false;
            _button.onClick.AddListener(ShowAd);
        }
    }

    void Update()
    {
        if (_button == null) return;

        _button.interactable = (
            Advertisement.isInitialized &&
            Advertisement.IsReady(placementId)
        );
    }

    public void ShowAd()
    {
        Advertisement.Show(placementId);
    }
}
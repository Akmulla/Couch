using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueText : MonoBehaviour
{
    public static ContinueText continueComponent;
    // Use this for initialization
    void Awake ()
    {
        continueComponent = this;
    }
	
	public void EnableText(bool isEnabled)
    {
        if (isEnabled)
            continueComponent.gameObject.SetActive(true);
        else
            continueComponent.gameObject.SetActive(false);
    }
}

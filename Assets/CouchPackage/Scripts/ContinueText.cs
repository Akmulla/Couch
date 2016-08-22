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
        gameObject.SetActive(false);
    }
	
	public void EnableText(bool isEnabled)
    {
        //continueComponent.gameObject.transform.parent.gameObject.SetActive(isEnabled);
        //Debug.Log(isEnabled);
        gameObject.SetActive(isEnabled);
    }
}

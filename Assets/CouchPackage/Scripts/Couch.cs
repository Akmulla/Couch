using UnityEngine;
using System.Collections;

public class Couch : MonoBehaviour
{
    public enum movePhase {Menu, Reset,Idle, MoveUp, Rotate, Finish,FinishIdle,Overcome,Count };
    public static GameObject couch;
    public static movePhase phase;
    public static Sprite sprite;

    void Awake ()
    {
        couch = gameObject;
        sprite = GetComponent<SpriteRenderer>().sprite;
        phase = movePhase.Reset;
	}
}

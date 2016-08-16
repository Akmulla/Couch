using UnityEngine;
using System.Collections;

public class Couch : MonoBehaviour
{
    public enum movePhase {Menu, Reset,Idle, MoveUp, Rotate, Finish,FinishIdle,Overcome,Count };
    public static GameObject couch;
    public static movePhase phase;

    void Awake ()
    {
        couch = gameObject;
        phase = movePhase.Reset;
	}
}

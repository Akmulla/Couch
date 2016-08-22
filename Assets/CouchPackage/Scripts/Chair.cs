using UnityEngine;
using System.Collections;

public class Chair : MonoBehaviour
{
    public static Chair chair;
    public GameObject SecondChair;
    public static float chairWidth;
    public static float chairHeight;
    public static Bounds chairBox;
	
	void Awake ()
    {
        chair = this;
	}

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        chairBox = renderer.sprite.bounds;
        chairWidth = chairBox.extents.x*transform.localScale.x;
        chairHeight = chairBox.extents.y*transform.localScale.y;
    }

    public float SetChairPosition(float width,bool left)
    {
        float x = left ? Edges.leftEdge+chairWidth : Edges.rightEdge-chairWidth;
        transform.position = new Vector3(x, Edges.topEdge - chairHeight, 0.0f);
        transform.rotation = left ? Quaternion.Euler(0.0f, 0.0f, 0.0f) : 
            Quaternion.Euler(0.0f, 0.0f, 180.0f);
        return (Edges.topEdge - width - chairHeight*2.0f);
    }

    public void SetSecondChairPosition(float height,bool left)
    {
        SecondChair.SetActive(true);
        float x = left ? Edges.leftEdge + chairWidth : Edges.rightEdge - chairWidth;
        SecondChair.transform.position = new Vector3(x, Couch.couch.transform.position.y-height-chairHeight*2.0f-0.3f);
        SecondChair.transform.rotation = left ? Quaternion.Euler(0.0f, 0.0f, 0.0f) :
            Quaternion.Euler(0.0f, 0.0f, 180.0f);
    }
}

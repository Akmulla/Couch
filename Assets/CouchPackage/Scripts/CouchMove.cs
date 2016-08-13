using UnityEngine;
using System.Collections;

public class CouchMove : MonoBehaviour
{
    //Rigidbody2D rb;
    [SerializeField] float speed;
    delegate void UpdateDelegate();
    UpdateDelegate[] updateDelegates;
    float topTargetPosition;
    float sideTargetPosition;
    bool leftSide;
    float width;
    float height;
    Vector2 center;
    Sprite sprite;

    //bool rot = false;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;

        updateDelegates = new UpdateDelegate[(int)Couch.movePhase.Count];
        //rb = GetComponent<Rigidbody2D>();
        updateDelegates[(int)Couch.movePhase.Reset] = Reset;
        updateDelegates[(int)Couch.movePhase.Idle] = Idle;
        updateDelegates[(int)Couch.movePhase.MoveUp] = MoveUp;
        updateDelegates[(int)Couch.movePhase.Rotate] = Rotate;
        updateDelegates[(int)Couch.movePhase.Finish] = Finish;
        updateDelegates[(int)Couch.movePhase.FinishIdle] = FinishIdle;
        updateDelegates[(int)Couch.movePhase.Overcome] = Overcome;
    }

    protected void OnDestroy()
    {
        //Clean up all the updateDelegates
        if (updateDelegates != null)
        {
            for (int i = 0; i < (int)Couch.movePhase.Count; i++)
            {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (updateDelegates[(int)Couch.phase] != null)
        {
            updateDelegates[(int)Couch.phase]();
        }


    }
    void Reset()
    {
        leftSide = Random.value > 0.5f ? true : false; 

        ContinueText.continueComponent.EnableText(false);
        //Debug.Log("Reset");
        float scale = Random.Range(0.6f, 1.2f);
        transform.localScale = new Vector3(scale, scale,1.0f);

        width = sprite.bounds.extents.x * transform.localScale.x;
        height = sprite.bounds.extents.y * transform.localScale.y;

        topTargetPosition = Edges.topEdge - width;
        if (leftSide)
        {
            sideTargetPosition = Edges.leftEdge + height;
            transform.position = new Vector2(Edges.rightEdge - width, Edges.botEdge + height);
        }
        else
        {
            sideTargetPosition = Edges.rightEdge - height;
            transform.position = new Vector2(Edges.leftEdge + width, Edges.botEdge + height);
        }
        transform.rotation = Quaternion.identity;
        Couch.phase = Couch.movePhase.Idle;
    }
    void Idle()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                Couch.phase = Couch.movePhase.MoveUp;
        }
    }
    void MoveUp()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        if (Input.touchCount > 0)
        {
            if ((Input.GetTouch(0).phase == TouchPhase.Canceled) || (Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                center = new Vector2(leftSide ? Edges.leftEdge+height : Edges.rightEdge - height, 
                    transform.position.y);
                Couch.phase = Couch.movePhase.Rotate;
                //rb.velocity = Vector2.zero;
            }
        }
        if (transform.position.y > Edges.topEdge)
            Couch.phase = Couch.movePhase.Overcome;
    }
    void Rotate()
    { 
        transform.RotateAround(center, Vector3.forward, leftSide? 5.0f : -5.0f);
        if (leftSide ? (transform.rotation.eulerAngles.z > 89.0f) : (transform.rotation.eulerAngles.z < 271.0f))
        {
            Couch.phase = Couch.movePhase.Finish;
        }
    }
    void Finish()
    {
        Debug.Log(topTargetPosition - transform.position.y);
        if (Mathf.Abs(topTargetPosition - transform.position.y)<0.25f)
        {
            Score.scoreComponent.IncreaseScore(1);
        }
        else
        {
            Score.scoreComponent.ResetScore();
        }
        Couch.phase = Couch.movePhase.FinishIdle;
    }
    void FinishIdle()
    {
        ContinueText.continueComponent.EnableText(true);
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                ContinueText.continueComponent.EnableText(false);
                Couch.phase = Couch.movePhase.Reset;
            }
        }
    }
    void Overcome()
    {
        Couch.phase = Couch.movePhase.Reset;
        Score.scoreComponent.ResetScore();
    }
}

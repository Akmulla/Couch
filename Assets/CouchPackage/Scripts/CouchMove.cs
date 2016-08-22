using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class CouchMove : MonoBehaviour
{
    //Rigidbody2D rb;
    [SerializeField] float baseSpeed;
    float speed;
    delegate void UpdateDelegate();
    UpdateDelegate[] updateDelegates;
    float topTargetPosition;
    //float sideTargetPosition;
    bool leftSide;
    float width;
    float height;
    Vector2 center;
    
    public GameObject man;
    Animator anim;

    //public GameObject menu;

    //bool rot = false;
    // Use this for initialization
    void Start()
    {
        anim = man.GetComponent<Animator>();

        updateDelegates = new UpdateDelegate[(int)Couch.movePhase.Count];
        //rb = GetComponent<Rigidbody2D>();
        updateDelegates[(int)Couch.movePhase.Menu] = Menu;
        updateDelegates[(int)Couch.movePhase.Reset] = Reset;
        updateDelegates[(int)Couch.movePhase.Idle] = Idle;
        updateDelegates[(int)Couch.movePhase.MoveUp] = MoveUp;
        updateDelegates[(int)Couch.movePhase.Rotate] = Rotate;
        updateDelegates[(int)Couch.movePhase.Finish] = Finish;
        updateDelegates[(int)Couch.movePhase.FinishIdle] = FinishIdle;
        updateDelegates[(int)Couch.movePhase.Overcome] = Overcome;

        Reset();
        Couch.phase = Couch.movePhase.Idle;
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
        //Debug.Log(Couch.phase);
        if (updateDelegates[(int)Couch.phase] != null)
        {
            updateDelegates[(int)Couch.phase]();
        }


    }

    void Menu()
    {
        //menu.SetActive(true);
    }
    void Reset()
    {
        
        anim.SetBool("Move", false);
        Ads.ads.Increase();
        leftSide = Random.value > 0.5f ? true : false; 

        ContinueText.continueComponent.EnableText(false);
        float scale = Random.Range(0.6f, 1.2f);
        Couch.couch.transform.localScale = new Vector3(scale, scale,1.0f);
        speed = baseSpeed / scale;

        width = Couch.sprite.bounds.extents.x * Couch.couch.transform.localScale.x;
        height = Couch.sprite.bounds.extents.y * Couch.couch.transform.localScale.y;


        man.transform.localPosition = new Vector3(0.0f, -height, 0.0f);
        Couch.couch.transform.localPosition = Vector3.zero;
        //topTargetPosition = Edges.topEdge - width;
        topTargetPosition = Chair.chair.SetChairPosition(width, leftSide);
        if (leftSide)
        {
            transform.position = new Vector2(Edges.rightEdge - width, Edges.botEdge + height);
        }
        else
        {
            transform.position = new Vector2(Edges.leftEdge + width, Edges.botEdge + height);
        }
        transform.rotation = Quaternion.identity;
        Couch.phase = Couch.movePhase.Idle;
    }
    void Idle()
    {
        if ((Input.touchCount > 0)&&(!Advertisement.isShowing))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                Couch.phase = Couch.movePhase.MoveUp;
        }
    }
    void MoveUp()
    {
        anim.SetBool("Move", true);
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
        //MoveMan();
        if (transform.position.y > Edges.topEdge)
            Couch.phase = Couch.movePhase.Overcome;
    }
    void Rotate()
    {
        //if (Couch.sprite.bounds.Intersects(Chair.chairBox))
        //Couch.phase = Couch.movePhase.Overcome;
        transform.RotateAround(center, Vector3.forward, leftSide? 5.0f : -5.0f);
        //MoveMan();
        if (leftSide ? (transform.rotation.eulerAngles.z > 89.0f) : (transform.rotation.eulerAngles.z < 271.0f))
        {
            Couch.phase = Couch.movePhase.Finish;
        }
    }
    void Finish()
    {
        anim.SetBool("Move", false);
        if (Mathf.Abs(topTargetPosition - transform.position.y)<0.25f)
        {
            Chair.chair.SetSecondChairPosition(height, leftSide);
            Score.scoreComponent.IncreaseScore(1);
            ContinueText.continueComponent.EnableText(true);
        }
        else
        {
            Score.scoreComponent.Finish();
            Score.scoreComponent.ResetScore();
        }
        Couch.phase = Couch.movePhase.FinishIdle;
        
    }
    void FinishIdle()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                ContinueText.continueComponent.EnableText(false);
                Score.scoreComponent.Continue();
                Chair.chair.SecondChair.SetActive(false);
                Couch.phase = Couch.movePhase.Reset;
                
            }
        }
    }
    void Overcome()
    {
        anim.SetBool("Move", false);
        Score.scoreComponent.Finish();
        Score.scoreComponent.ResetScore();
        Couch.phase = Couch.movePhase.FinishIdle;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Couch.phase = Couch.movePhase.Overcome;
    }
    
}

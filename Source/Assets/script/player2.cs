using UnityEngine;
using System.Collections;

public class player2 : MonoBehaviour
{

    private RaycastHit objhit;
    private GameObject obj_down;
    private GameObject obj_down_pre;
    private GameObject prob_down;
    private GameObject obj_ctrl;
    private float time_die_begin;
    private float time_die_end;
    public bool alive;//<----edited
    private controller ctrl;
    private AudioSource[] audio;
    public AudioClip clip_hitstar;
    public AudioClip clip_hitplayer;
    public AudioClip clip_die;

    // Use this for initialization
    void Start()
    {
        alive = false;
        audio = GetComponents<AudioSource>();
        obj_ctrl = GameObject.Find("controller");
        ctrl = obj_ctrl.GetComponent<controller>();
       
        obj_down_pre = null;
        if (GameObject.Find("prob_down2"))
        {
            prob_down = GameObject.Find("prob_down2");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (Input.GetKey(KeyCode.UpArrow))//前进
            {
                transform.Translate(10 * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))//后退
            {
                transform.Translate(-10 * Time.deltaTime, 0, 0);
            }
            if (prob_down)
            {

                //如果射到
                if (Physics.Raycast(transform.position, prob_down.transform.position - transform.position, out objhit, 100, 1 << LayerMask.NameToLayer("fan")))
                {

                    obj_down = objhit.collider.gameObject;
                    if (!obj_down_pre || (obj_down && obj_down_pre && obj_down.tag != obj_down_pre.tag))
                    {
                        align();
                        obj_down_pre = obj_down;

                    }
                }
                //如果射不到
                else
                {
                    if ((!Physics.Raycast(transform.parent.TransformPoint(transform.localPosition + new Vector3(0.05f, 0, 0)), prob_down.transform.position - transform.position, out objhit, 100, 1 << LayerMask.NameToLayer("fan"))) && (!Physics.Raycast(transform.parent.TransformPoint(transform.localPosition - new Vector3(0.05f, 0, 0)), prob_down.transform.position - transform.position, out objhit, 100, 1 << LayerMask.NameToLayer("fan"))))
                    {
                        die();
                    }
                }

            }
        }
        else
        {
            //do nothing
        }

    }

    public void die()
    {
        audio[0].clip = clip_die;
        audio[0].PlayOneShot(clip_die);
        Debug.Log("die2");
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        collider.enabled = false;
        transform.parent = null;
        alive = false;
    }
    private void align()
    {
        bool flip = false;
        //Debug.Log(Mathf.Abs(obj_down.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y));
        if (Mathf.Abs(obj_down.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) % 360 > 90)
        {
            //Debug.Log("flip is true");
            flip = true;
        }
        transform.rotation = obj_down.transform.rotation;
        transform.parent = obj_down.transform;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

        if (flip)
        {
            //Debug.Log("flip rotate");
            transform.Rotate(0, 180, 0);
        }
    }

   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")//layer == 1 << LayerMask.NameToLayer("player"))//
        {
            audio[1].clip = clip_hitplayer;
            audio[1].PlayOneShot(clip_hitplayer);

            if (ctrl.game_mode == 1)
            {
                Debug.Log("Boom2");
                die();
            }
            else if (ctrl.game_mode == 2)
            {
                if (ctrl.score > ctrl.score_2nd)
                {
                    die();
                }
            }
        }
        if (other.gameObject.name == "star")
        {
            audio[1].clip = clip_hitstar;
            audio[1].PlayOneShot(clip_hitstar);
            GameObject star = other.gameObject;
            Destroy(star);

            //controller ctrl = obj_ctrl.GetComponent<controller>();
            if (ctrl.game_mode == 1)
            {
                ctrl.ScorePlus();
            }
            else
            {
                ctrl.Score2Plus();
            }
        }
    }
}

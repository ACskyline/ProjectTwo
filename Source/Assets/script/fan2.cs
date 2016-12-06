using UnityEngine;
using System.Collections;

public class fan2 : MonoBehaviour
{

    public GameObject obj_ctrl;
    public controller ctrl;

    // Use this for initialization
    void Start()
    {
        obj_ctrl = GameObject.Find("controller");
        ctrl = obj_ctrl.GetComponent<controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ctrl.game_mode != 0)
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }
}

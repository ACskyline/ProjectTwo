using UnityEngine;
using System.Collections;

public class fan : MonoBehaviour {
    public float rotate_speed;
    public controller controller;
    public fan(float r_speed)
    {
        rotate_speed = r_speed;
    }
    public void linkControll()
    {
        controller = GameObject.Find("controller").GetComponent<controller>();
    }
}

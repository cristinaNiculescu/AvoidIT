using UnityEngine;
using System.Collections;

public class ControlHandler : MonoBehaviour {

    GameObject p;
    Rigidbody2D prb2d;
    public string pAxis;
    float v;
    public int direction;
    public string playerName;

    public float speed;
    RaycastHit2D hitInfo;

    bool move = false;

    // Use this for initialization
    void Start() {

        p = GameObject.Find(playerName);
        prb2d = p.GetComponent<Rigidbody2D>();

    }

    /// <summary>
    /// Controls. Simplest way not to use the Standard Assets CrossPlatformInput. Test controls with keyboard.
    /// On pc the movement "adds up".
    /// </summary>
    void Update()
    {
       
        v = Input.GetAxis(pAxis);
        if (p != null && prb2d != null)
        {
            speed = p.GetComponent<PlayerBehavior>().speed;
            if (v != 0 )
            {
                prb2d.velocity = v * Vector2.up * speed * 10;
            }

           if (move == true)
            {
                prb2d.velocity = 1.05f * Vector2.up * speed * 10*direction;
                // p.transform.Translate(Vector2.left * direction*speed/10);
                //applies a constant translate movement as long as move is set to true. 
            }
        }

    }

    public void OnPointerDown()
    {
        move = true;
    }
    public void OnPointerUp()
    {
        move = false;
    }

}

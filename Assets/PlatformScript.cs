using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Rigidbody2D rigitBody;
    public int speedMultiplier = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            rigitBody.velocity = UnityEngine.Vector2.left * speedMultiplier;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            rigitBody.velocity = UnityEngine.Vector2.right * speedMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) == true)
        {
            rigitBody.velocity = UnityEngine.Vector2.zero;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) == true)
        {
            rigitBody.velocity = UnityEngine.Vector2.zero;
        }
    }
}

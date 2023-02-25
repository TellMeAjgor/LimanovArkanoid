using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    #region Singleton

    private static PlatformScript _instance;
    public static PlatformScript Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 minScreenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector2(0, 0));
        UnityEngine.Vector2 maxScreenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector2(Screen.width, Screen.height));

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new UnityEngine.Vector2(Mathf.Clamp(mousePosition.x, minScreenBounds.x + 1, maxScreenBounds.x - 1), transform.position.y);

    }
}

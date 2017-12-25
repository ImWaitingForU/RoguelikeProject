using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MapManager manager;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        if (Input.GetKeyDown(KeyCode.W) && y < manager.rows - 2)
        {
            transform.Translate(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && y >= 2)
        {
            transform.Translate(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && x >= 2)
        {
            transform.Translate(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && x < manager.cols - 2)
        {
            transform.Translate(1, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MapManager manager;
    public float smoothing = 1;
    //控制按钮0.5s只能响应一次
    public float restTime = 0.1f;
    public float restTimer = 0;

    //起始坐标为1,1
    private Vector2 targetPos = new Vector2(1, 1);
    private Rigidbody2D rigidBody;
    private BoxCollider2D col;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //rigidBody.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));
        //代替插值移动。让移动更加顺滑    
        rigidBody.MovePosition(targetPos);

        restTimer += Time.deltaTime;
        //当restTimer>restTime才会检测键盘移动
        if (restTimer < restTime)
        {
            return;
        }
        //上下左右移动 up-v:1 down-v:-1 left-x:-1 right-x:1
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //只允许水平/垂直移动
        if (h > 0)
        {
            v = 0;
        }
        else if (v > 0)
        {
            h = 0;
        }

        if (h != 0 || v != 0)
        {
            //检测碰撞,先禁用自身,防止射线接触到自身的刚体
            col.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            col.enabled = true;
            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
            }
            else
            {
                switch (hit.transform.tag)
                {
                    case "OutWall": break;
                    case "Wall":
                        animator.SetTrigger("Attack");
                        hit.collider.SendMessage("TakeDamage");
                        break;
                    default:
                        break;
                }
            }
            restTimer = 0;
        }
    }
}

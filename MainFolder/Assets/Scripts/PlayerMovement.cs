using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static int slayAmount = 0;
    #region 物理相关
    private Rigidbody2D rb;

    float moveH, moveV;
    #endregion

    //通过布尔值判断是否在冲刺
    bool isDashing;
    //判断当前是否有残影协程正在生成
    bool coroStarted = false;

    [Header("移动速度")]
    //移动速度
    public float moveSpeed;

    [Header("冲刺速度")]
    //冲锋速度
    public float dashSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDashing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dash();
          
        }
        else
        {
            isDashing = false;
            rb.velocity = new Vector2(moveH * moveSpeed, moveV * moveSpeed);
            
        }
        Flip();
    }

    private void Flip()
    {
        if (moveH > 0)
        { 
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if (moveH < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    private void Dash()
    {
        

        rb.velocity = new Vector2(moveH * dashSpeed, moveV * dashSpeed);

        if (!coroStarted)
        {

            StartCoroutine(GenerateRems());
            coroStarted = true;
        }

    }


    IEnumerator GenerateRems()
    {
        while (true)
        {
            //对对象池中物体进行弹栈
            RemanentController.instance.GetFromPool();

            yield return new WaitForSeconds(0.13f);

            if (!isDashing)
            {
                break;
            }
        }
        coroStarted = false;
        yield return 0;
    }
}

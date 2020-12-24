using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("当前的生命值")]
    [SerializeField]
    private float hp;

    Transform target;

    [Header("基础移速")]
    [SerializeField] 
    private float enemyMoveSpeed = 1;

    [Header("最大生命值")]
    [SerializeField] 
    private float maxHp = 100;

    [Header("受伤状态显示的相关属性")]
    [SerializeField]
    float hurtLast, hurtCounter;

    [Header("加速度")]
    [SerializeField] 
    private float accSpeed = 1.3f;

    private SpriteRenderer sr;


    private void Start()
    {

        target = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        
    }

    private void OnEnable()
    {
        hp = maxHp;
    }

    private void Update()
    {
        //如果现在是第二阶段或第三阶段，对敌人进行随机加速处理
        if (HudPanel.instance.flag == 2)
        {
            enemyMoveSpeed = Random.Range(-accSpeed / 2  + 1, accSpeed + 1);
        }
        else if(HudPanel.instance.flag == 3)
        {
            enemyMoveSpeed = Random.Range(-accSpeed / 3 + 1, accSpeed * 1.1f + 1);
        }
        FollowPlayer();

        if (hurtCounter <= 0)
        {
            sr.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //生成战败的界面
            HudPanel.instance.GenerateFailedPanel();
        }
    }
 
    /// <summary>
    /// 受伤方法
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(float damage)
    {
        hp -= damage;
        //怪物受伤时变白
        HurtShader();
        //如果怪物被击杀
        if (hp <= 0)
        {
            //HUD显示得分
            HudPanel.instance.OnSlayChanged(++PlayerMovement.slayAmount);
           //将被击杀的怪物收回对象池
            BirdController.instance.ReturnToPool(gameObject);
        }
    }
    /// <summary>
    /// 使怪物跟随玩家移动
    /// </summary>
    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position,target.position,enemyMoveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 怪物受伤时变白
    /// </summary>
    private void HurtShader()
    {
        sr.material.SetFloat("_FlashAmount",1);
        hurtCounter = hurtLast;
       
    }
    
}

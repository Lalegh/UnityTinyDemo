using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private float hp;

    Transform target;

    [SerializeField] private float enemyMoveSpeed = 1;

    [SerializeField] private float maxHp = 100;

    [SerializeField]
    float hurtLast, hurtCounter;

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
        Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Player"))
        {
            HudPanel.instance.GenerateFailedPanel();
        }
    }
 
    public void Damaged(float damage)
    {
        hp -= damage;
        HurtShader();
        if (hp <= 0)
        {
            //HUD显示得分
            HudPanel.instance.OnSlayChanged(++PlayerMovement.slayAmount);
           
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

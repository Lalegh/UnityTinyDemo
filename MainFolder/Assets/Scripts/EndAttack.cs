using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttack : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Damaged(20);

            #region StrikeBackEffect
            Vector2 offset = collision.transform.position - transform.position;
            //敌人与刀光之间的距离差的归一化,表征方向
            offset = offset.normalized;
            //collision.transform.position = new Vector2(collision.transform.position.x +
            //    offset.x,collision.transform.position.y+offset.y);
            StartCoroutine(StrikeBack(offset,collision.transform));
            #endregion
        }
    }
    /// <summary>
    /// 刀光结束的最后一帧，添加帧事件setActive(false)
    /// </summary>
    public void  EndAttack1()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 控制敌人击退的协程
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="transform"></param>
    /// <returns></returns>
    IEnumerator StrikeBack(Vector2 offset,Transform transform)
    {
        int counter = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            counter++;

            transform.position = new Vector2(transform.position.x + offset.x* 10 * Time.deltaTime * Mathf.Lerp(0, 1, 0.7f),
                transform.position.y + offset.y*Time.deltaTime * 10 * Mathf.Lerp(0, 1, 0.7f));
            
            if (counter == 200)
            {
                break;
            }
        }
        yield return 0;
    }
}

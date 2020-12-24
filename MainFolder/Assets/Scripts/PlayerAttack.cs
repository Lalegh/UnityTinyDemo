using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&HudPanel.instance.flag == 0)
        {
            ///
            Attack();
        }
    }

    void Attack()
    {
        #region 通过反正切来算出夹角
        Vector2 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        //计算刀光的倾斜角度
        transform.rotation = Quaternion.Euler(0, 0, angle);
        #endregion

        //激活刀光
        transform.GetChild(0).gameObject.SetActive(true);
    }

  
}

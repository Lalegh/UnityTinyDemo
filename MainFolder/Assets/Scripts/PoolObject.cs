using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    SpriteRenderer sr;

    Transform target;

    public Vector4 a;

 
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        a = sr.material.color;
    }
    private void OnEnable()
    {
        sr.material.color = new Color(1, 1, 1, 1);
        sr.color = new Color(1,1,1,1);

        //设置父物体
        transform.SetParent(target);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.SetParent(null);

        //开启渐变色协程
        StartCoroutine(Shade());
        
    }
   


    IEnumerator Shade()
    {
        int counter = 0;
        while (true)
        {
            //设置透明度渐变
            sr.color -= new Color(0,0,0,0.2f);
            
            yield return new WaitForSeconds(0.1f);

            counter++;
            if (counter == 5)
            {
                RemanentController.instance.ReturnToPool(gameObject);
                break;
            }
        }
        yield return 0;
    }
}

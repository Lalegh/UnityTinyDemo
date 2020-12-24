using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;

    //敌人游戏对象
    GameObject birds;

    //玩家游戏对象
    GameObject player;
    //用于对象池的临时容器
    Stack<GameObject> birdsHome;

    [Header("生成怪物的间隔")]
    public float firstTimeInterval = 1.5f;//第一阶段下生成怪物的速度
    public float secondTimeInterval = 2f;//第二阶段下生成怪物的速度
    public float thirdTimeInterval = 2.5f;//第三阶段下生成怪物的速度

    private void Awake()
    {
        instance = this;
        birdsHome = new Stack<GameObject>();
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    IEnumerator  Start()
    {
        birds = Resources.Load<GameObject>("Enemy");
        //控制当前游戏节奏的变量
        int counter = 0;

        while (true)
        {
            yield return new WaitForSeconds(firstTimeInterval);
            InstantiateNGenerate();
            counter++;

            if (counter == 10)
            {
                //跳出本层循环，进入下一个循环，怪物生成的第二阶段
                HudPanel.instance.flag = 1;
                break;
            }
        }

        while (true)
        {
            yield return new WaitForSeconds(secondTimeInterval);

            GameObject temp = GetFromPool();
            temp.transform.SetParent(transform);
            //birds位置初始化
            birds.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));

            counter++;
            if (counter == 14)
            {
                //跳出本层循环，进入下一个循环，怪物生成的第三阶段
                HudPanel.instance.flag = 2;
                break;
            }
        }
        GameObject wall = Resources.Load<GameObject>("TotalWall");
        Instantiate(wall, player.transform.position, Quaternion.identity) ;
        while (true)
        {
            yield return new WaitForSeconds(thirdTimeInterval);

            GameObject temp = GetFromPool();
            temp.transform.SetParent(transform);
            //birds位置初始化
        }

    }

    /// <summary>
    /// 通过对象池生成一个物体
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromPool()
    {
        if (birdsHome.Count == 0)
        {
            GameObject temp = Resources.Load<GameObject>("Enemy");
            temp = Instantiate(temp);
            birdsHome.Push(temp);    
        }
        GameObject res = birdsHome.Pop();
        res.SetActive(true);
        return res;
    }

    /// <summary>
    /// 通过对象池回收一个物体
    /// </summary>
    /// <param name="go"></param>
    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        birdsHome.Push(go);
    }

    public GameObject InstantiateNGenerate()
    {
        GameObject go = GetFromPool();
        //设置父物体
        go.transform.SetParent(transform);
        //birds位置初始化
        Vector2 temp = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        //如果怪物生成位置距离玩家过近
        while(Vector2.Distance(Camera.main.ViewportToWorldPoint(temp),player.transform.position) < 3f)
        {
            //重新计算位置
            temp = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        go.transform.position = Camera.main.ViewportToWorldPoint(temp);

        return go;
    }


   
}

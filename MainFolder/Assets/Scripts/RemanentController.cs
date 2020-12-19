using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemanentController : MonoBehaviour
{
    public static RemanentController instance;


    //用来存放预设残影
    Stack<GameObject> tempStack;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        tempStack = new Stack<GameObject>();
    }

  public  GameObject GetFromPool()
    {
        if (tempStack.Count == 0)
        {
            GameObject dash = Resources.Load<GameObject>("player_dash");
            GameObject res = Instantiate(dash);

            res.name = "player_dash";
            //激活对象
            dash.SetActive(true);

            //设置父子关系
            //dash.transform.parent = GameObject.FindWithTag("Player").transform;


            tempStack.Push(dash);
        }
        
        //弹栈
        GameObject temp = tempStack.Pop();
        temp.SetActive(true);

        return temp;
    }

   public void ReturnToPool(GameObject target)
    {
        //灭活对象
        target.SetActive(false);
        //压栈
        tempStack.Push(target);
    }

   
}

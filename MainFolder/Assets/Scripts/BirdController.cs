using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;

    GameObject birds;

    Stack<GameObject> birdsHome;

    [Header("生成怪物的间隔")]
    public float timeInterval = 1.5f;
    private void Awake()
    {
        instance = this;
        birdsHome = new Stack<GameObject>();
    }

    // Start is called before the first frame update
    IEnumerator  Start()
    {
        birds = Resources.Load<GameObject>("Enemy");

        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            GameObject temp = GetFromPool();
            temp.transform.SetParent(transform);
            //birds初始化
            birds.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f,1f),Random.Range(0f,1f)));

        }

    }

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
    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        birdsHome.Push(go);
    }


   
}

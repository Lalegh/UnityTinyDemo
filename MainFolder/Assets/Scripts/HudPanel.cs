using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class HudPanel:MonoBehaviour
{

    #region 单例
    public static HudPanel instance;
    #endregion

    //显示击杀数的Text组件
    Text slay;
    Button buttonl;

    private void Awake()
    {
        instance = this;
        slay = transform.Find("SlayCount").GetComponent<Text>();
    }
    

   public  void OnSlayChanged(int input)
    {

        slay.text = input.ToString();
    }
   public void OnSlayChanged(string input)
    {
        slay.text = input;
    }

    public void GenerateFailedPanel()
    {
        Time.timeScale = 0;
        GameObject temp = Instantiate(Resources.Load<GameObject>("FailedPanel"), GameObject.FindWithTag("MainCanvas").transform);




    }
}

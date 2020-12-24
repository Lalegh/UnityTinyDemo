using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// 对主摄像机和虚拟摄像机进行操作
/// </summary>
public class MainCameraColorController : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ColorFading());

        StartCoroutine(DepthAdjusting());
    }
    /// <summary>
    /// 实现相机背景色渐变的效果，从天蓝色渐变至深色
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorFading()
    {
        int counter = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            //摄像机背景色渐变，从蓝色渐变至深色
            Camera.main.backgroundColor -= new Color(0,0.002f,0.004f,0);

            counter++;
            //三十秒后退出颜色控制
            if (counter == 300)
            {
                break;
            }
        }
        
    }

    /// <summary>
    /// 渐变虚拟相机的OrthographicDepth
    /// </summary>
    /// <returns></returns>
    IEnumerator DepthAdjusting()
    {
        int counter = 0;

        //获取虚拟相机
        CinemachineVirtualCamera virtualCamera = 
            GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            //正交深度渐变
            virtualCamera.m_Lens.OrthographicSize -= 0.0084f;

            counter++;

            if (counter == 300)
            {
                break;
            }
        }
    }

}

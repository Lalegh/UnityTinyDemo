using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }
    // Start is called before the first frame update
   IEnumerator  Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            myText.color -= new Color(0, 0, 0,0.01f);
            if (myText.color == new Color(0, 0, 0, 0))
            {
                break;
            }

        }
        Destroy(gameObject);
        yield return 0;
    }


}

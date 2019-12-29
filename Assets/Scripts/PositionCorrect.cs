using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCorrect : MonoBehaviour
{
    public enum agliments
    {
        left = 0,
        right = 1
    }
    public agliments targetAgliment;
    // Start is called before the first frame update
    void Start()
    {
        if (targetAgliment == 0)
        {
            this.transform.position += new Vector3(-(Screen.width-1024) / 2 /100,0, 0);
        }
        else
        {
            this.transform.position += new Vector3((Screen.width - 1024) / 2 /100, 0, 0);
        }
    }
    int scaleCoficient() 
    {
        return 768 / Screen.height;
    }
}

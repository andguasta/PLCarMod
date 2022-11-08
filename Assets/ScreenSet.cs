using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;        
    }
}

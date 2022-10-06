using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMod : MonoBehaviour
{
    [SerializeField] private bool isModActive;
    // Start is called before the first frame update
    void Awake()
    {
        if(isModActive)
        {
            PlayerPrefs.SetInt("currency", 10000000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

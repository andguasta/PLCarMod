using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    private controller carController;

    public AK.Wwise.Event engine_on;
    public AK.Wwise.Event engine_off;
    public AK.Wwise.RTPC engine_rpm;

    //public AK.Wwise.Event startSmoke;
    //public AK.Wwise.Event stopSmoke;

    //public AK.Wwise.Event handbrake_on;


    //public AK.Wwise.RTPC carSpeed_kmh;

    //public AK.Wwise.RTPC gear_number;
    //public AK.Wwise.Event gearUp;
    //public AK.Wwise.Event gearDown;

    //public AK.Wwise.RTPC nitro;


    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
        engine_on.Post(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(carController.getEngineRpm());
        engine_rpm.SetValue(this.gameObject, carController.getEngineRpm());

    }

    private void OnApplicationQuit()
    {
        engine_off.Post(this.gameObject);
    }
}

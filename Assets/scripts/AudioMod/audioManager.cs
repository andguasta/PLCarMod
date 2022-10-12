using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class audioManager : MonoBehaviour
{
    private controller carController;
    private inputManager carInputManager;

    [SerializeField] private AK.Wwise.Event engineOn;
    [SerializeField] private AK.Wwise.Event engineOff;

    [SerializeField] private AK.Wwise.RTPC engineRpm;
    [SerializeField] private AK.Wwise.RTPC carSpeedKmh;

    [SerializeField] private AK.Wwise.Event handbrakeTriggered;

    [Header("Gears")]
    [SerializeField] private AK.Wwise.RTPC gearNumber;
    [SerializeField] private AK.Wwise.Event gearUp;
    [SerializeField] private AK.Wwise.Event gearDown;


    //public AK.Wwise.Event startSmoke;
    //public AK.Wwise.Event stopSmoke;

    //public AK.Wwise.Event handbrake_on;


    //public AK.Wwise.RTPC nitro;


    void Start()
    {
        carController = this.gameObject.GetComponent<controller>();
        carInputManager = this.gameObject.GetComponent<inputManager>();

        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "awakeScene")
        //{
        //    engineOn.Post(this.gameObject);
        //}

        carController.gearUpEvent.AddListener(OnGearUp);
        carController.gearDownEvent.AddListener(OnGearDown);
        carInputManager.handbrakeEvent.AddListener(OnHandbrakeStart);
        carInputManager.engineStartEvent.AddListener(OnEngineStart);
        carInputManager.engineStopEvent.AddListener(OnEngineStop);
    }
 
    void Update()
    {       
        engineRpm.SetValue(this.gameObject, carController.getEngineRpm());
        carSpeedKmh.SetValue(this.gameObject, carController.getCarSpeed());
        gearNumber.SetValue(this.gameObject, carController.getGearNum());
    }

    private void OnDestroy()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "awakeScene")
        {
            engineOff.Post(this.gameObject);
        }
    }

    public void OnEngineStart()
    {
        engineOn.Post(this.gameObject);
    }

    public void OnEngineStop()
    {
        engineOff.Post(this.gameObject);
    }

    public void OnGearUp()
    {
        gearUp.Post(this.gameObject);
    }

    public void OnGearDown()
    {
        Debug.Log("OnGearDown");
        gearDown.Post(this.gameObject);
    }

    public void OnHandbrakeStart()
    {
        handbrakeTriggered.Post(this.gameObject);
    }
}

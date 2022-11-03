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
    [SerializeField] private AK.Wwise.RTPC engineRpmScaled;
    [SerializeField] private AK.Wwise.RTPC carSpeedKmh;
    

    [SerializeField] private AK.Wwise.Event handbrakeTriggered;
    [SerializeField] private AK.Wwise.Event skidStart;
    [SerializeField] private AK.Wwise.Event skidStop;

    [Header("Gears")]
    [SerializeField] private AK.Wwise.RTPC gearNumber;
    [SerializeField] private AK.Wwise.Event gearUp;
    [SerializeField] private AK.Wwise.Event gearDown;

    [Header("IntegrationParameters")]
    [SerializeField] private float skidResponse;

    private bool isSkidding;
    private float skidTime = 0.0f;

    //public AK.Wwise.Event startSmoke;
    //public AK.Wwise.Event stopSmoke;

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

        carController.skidStartEvent.AddListener(OnSkidStart);
        carController.skidStartEvent.AddListener(OnSkidStop);

        isSkidding = false;

        carSwitchSet(carController.getCarName());
    }

    void carSwitchSet(string carName)
    {
        string formattedName = carName.Replace(' ', '_');
        Debug.Log(formattedName);
        AkSoundEngine.SetSwitch("Car_Name", formattedName, this.gameObject);
    }
 
    void Update()
    {       
        engineRpm.SetValue(this.gameObject, carController.getEngineRpm());
        Debug.Log("NOTScaled: " + engineRpm.GetValue(gameObject));
        Debug.Log("SCALED: " + engineRpmScaled.GetValue(gameObject));
        engineRpmScaled.SetValue(this.gameObject, scaleRpm(carController.getEngineRpm()));
        carSpeedKmh.SetValue(this.gameObject, carController.getCarSpeed());
        gearNumber.SetValue(this.gameObject, carController.getGearNum());


        if(isSkidding) checkSkid();

    }

    private void checkSkid()
    {
        skidTime += Time.deltaTime;
        if (skidTime > skidResponse)
        {
            skidStop.Post(this.gameObject);           
            isSkidding = false;
            skidTime = 0.0f;
        }
    }

    //This function scales the rpm to a more manageable range
    //at the moment it's a dirty solution 
    private float scaleRpm(float inputRpm)
    {
        return (inputRpm < 1200) ? inputRpm : (float)(inputRpm * 0.75);  
    }

    public void OnSkidStart()
    {
        //Debug.Log("OnSkidStart");
        if (!isSkidding)
        {
            skidStart.Post(this.gameObject);            
            isSkidding = true;
        }
        else
        {
            skidTime = 0.0f;
        }
    }

    private void OnDestroy()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "awakeScene")
        {
            engineOff.Post(this.gameObject);
        }
        if (isSkidding)
        {
            skidStop.Post(this.gameObject);
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
        //Debug.Log("OnGearDown");
        gearDown.Post(this.gameObject);
    }

    public void OnHandbrakeStart()
    {
        handbrakeTriggered.Post(this.gameObject);
    }
   
    public void OnSkidStop()
    {
        
    }
}

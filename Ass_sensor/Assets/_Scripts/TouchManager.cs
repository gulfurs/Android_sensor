using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System;
using TMPro;

public class TouchManager : MonoBehaviour
{
    
    // [SerializeField] private GameObject player;
    // private PlayerInput playerInput;

    // private InputAction touchPositionAction;
    // private InputAction touchPressAction;

    public TextMeshProUGUI accelerometerText;
    private bool isMeasuring = false;
    private float measureStartTime = 0f;
    private const float measurementDuration = 3.00f;
    private float timer = 0.0f;
    private int count = 0;

    private StreamWriter sw;
    private const string FILENAME = "accmeter_data.csv";

    private void Start(){
        // string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        // string filePath = Path.Combine(desktopPath, FILENAME);

        // sw = new StreamWriter(filePath);

        sw = new StreamWriter(Application.persistentDataPath + "/" + FILENAME);
        sw.WriteLine("Time,X,Y,X");
    }

    public void OnButtonClick(){

        if(isMeasuring) {
            isMeasuring = false;
        } else {
            isMeasuring = true;
            measureStartTime = Time.time;
            Debug.Log("Test");
            Debug.Log("isMeasuring = " + isMeasuring);
        }
    }

     private void FixedUpdate(){
        timer += 0.1f;
        
        if (isMeasuring){
            if(Time.time - measureStartTime > measurementDuration){
                isMeasuring = false;
                Debug.Log("ITS OVER");
                count = 0;
            }else {
            Vector3 accData = Input.acceleration;
            accelerometerText.text = "X: " + accData.x.ToString("0.000") +
            "\n Y: " + accData.y.ToString("0.000") +
            "\n Z: " + accData.z.ToString("0.000");
            
            
            sw.WriteLine ("{0},{1},{2,{3}" , Time.time, accData.x, accData.y, accData.z);
            Debug.Log("The Data: " + accData);
            Debug.Log("The Time: " + Time.time);
            Debug.Log("The count " + count);
                }
            }
        count ++;
        timer = 0.0f;
    }

    private void OnApplicatonQuit(){
        sw.Close();
    }

    private void Awake(){
        // playerInput = GetComponent<PlayerInput>();
        // touchPressAction = playerInput.actions["TouchPress"];
        // touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void OnEnable() {
        // touchPressAction.performed += TouchPressed;
    }

    private void OnDisable() {
        // touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        // float value = context.ReadValue<float>();
        // Debug.Log(value);

        // Vector3 new_position = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
        // new_position.z = player.transform.position.z;
        // player.transform.position = new_position;
    }

}

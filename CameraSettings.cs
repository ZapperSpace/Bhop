using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    public bool AxisRAW;

    [Header("Third-Person")]
    public GameObject ThirdPersonCamera;
    public float CameraMoveSpeed = 120.0f;
    public GameObject ObjectFollow;
    Vector3 FollowPosition;
    public float MaxClamp;
    public float MinClamp;
    public float inputSensitivity = 150.0f;
    [Header("First-Person")]
    public GameObject FirstPersonCamera;
    [Header("Variables")]
    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float finalInputX;
    [SerializeField] private float finalInputZ;
    [SerializeField] public float rotY = 0.0f;
    [SerializeField] public float rotX = 0.0f;
    [SerializeField] public bool FstPerson;
    [SerializeField] public bool ThrdPerson;
    [SerializeField] public float ActiveCamera;
    //PauseMenu pauseMenu;
    bool switch1;
    bool switch2;
    // Use this for initialization
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.visible = false;
        FstPerson = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (AxisRAW)
        {
            float inputX = Input.GetAxisRaw("RightStickHorizontal");
            float inputZ = Input.GetAxisRaw("RightStickVeritcal");

            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            finalInputX = inputX + mouseX;
            finalInputZ = inputZ + mouseY;
        }
        else
        {
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputZ = Input.GetAxis("RightStickVeritcal");

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            finalInputX = inputX + mouseX;
            finalInputZ = inputZ + mouseY;
        }


        if (ThrdPerson)
        {
            ThirdPersonCamera.SetActive(true);
            FirstPersonCamera.SetActive(false);
            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputZ * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, MinClamp, MaxClamp);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
        if (FstPerson)
        {
            FirstPersonCamera.SetActive(true);
            ThirdPersonCamera.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ThrdPerson == true)
            {
                FstPerson = true;
                ThrdPerson = false;
                ActiveCamera = 1;
            }
            else
            {
                ActiveCamera = 3;
                FstPerson = false;
                ThrdPerson = true;
            }
        }
    }
    private void LateUpdate()
    {

    }
    void CameraUpdater()
    {
        Transform target = ObjectFollow.transform;
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}

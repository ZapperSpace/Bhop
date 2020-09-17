using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    //gettingcomponets
    public GameObject UI;
    public PlayerController player;

    //texts
    [Header("Texts")]
    public TextMeshProUGUI speedtxt;
    public TextMeshProUGUI velocitytxt;
    // public TextMeshProUGUI axistxt;
    public TextMeshProUGUI playerbooltxt;
    public TextMeshProUGUI playeroffsettxt;

    //varibles
    float speed;
    float velocity;
    Vector3 axis;
    bool onground;
    bool inair;
    bool isRunning;
    bool isWalking;
    bool isCrouching;
    bool isStill;

    // Use this for initialization
    void Start()
    {
        //Speedtxt = GetComponent<TextMeshProUGUI>();
        UI.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //varibles
        isRunning = player.isRunning;
        speed = Mathf.Round(player.speed);
        velocity = Mathf.Round(player.velocity);
        axis = player.AxisPosition;
        onground = player.onGround;
        inair = player.inAir;
        isWalking = player.isWalking;
        isCrouching = player.isCrouching;
        if (speed <= 0)
        {
            isStill = true;
        }
        else
        {
            isStill = false;
        }

        //texts
        speedtxt.text = "Speed: " + speed;
        velocitytxt.text = "Velocity: " + velocity;
        if (onground)
        {
            playerbooltxt.text = ":OnGround:";
        }
        else if (inair)
        {
            playerbooltxt.text = ":InAir:";
        }
        else
        {
            playerbooltxt.text = ":NULL:";
        }
        if (isWalking)
        {
            playeroffsettxt.text = "(IsWalking)";
        }
        if (isCrouching && isWalking)
        {
            playeroffsettxt.text = "(IsWalking & IsCrouching)";
        }
        if (isRunning)
        {
            playeroffsettxt.text = "(IsRunning)";
        }
        if(isRunning && isCrouching)
        {
            playeroffsettxt.text = "(IsRunning && IsCrouching)";
        }
        if (isStill)
        {
            playeroffsettxt.text = "(Still)";
        }
        if (isStill && isCrouching)
        {
            playeroffsettxt.text = "(Still & IsCrouching)";
        }
        // axistxt = "(AxisPosition) " + axis;

    }
}

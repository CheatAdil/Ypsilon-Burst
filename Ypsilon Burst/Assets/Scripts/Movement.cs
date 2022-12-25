using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public VariableJoystick variableJoystick;
    private float force, rotation;
    private Vector3 currentEulerAngles;
    private Rigidbody rb;
    [SerializeField] private GameObject trail;
    private ScoreManager scoreManager;
    private GameObject spawnManager;
    private bool died;
    private int timer;
    private Weapons weapons;
    private Difficulty difficulty;
    public void Start()
    {
        timer = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        trail.SetActive(false);
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        spawnManager = GameObject.Find("SpawnManager");
        weapons = this.GetComponent<Weapons>();
        this.name = "Player";
        fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        difficulty = GameObject.Find("DifficultyManager").GetComponent<Difficulty>();
    }
    void FixedUpdate()
    {
        if (fixedJoystick == null) Start();
        timer++;
        if (weapons.energy >= difficulty.energySpend)
        {
            Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            //transform.Rotate(0, rotation * Time.fixedDeltaTime, 0);
            if (fixedJoystick.Vertical != 0f || fixedJoystick.Horizontal != 0f)
            {
                if (timer >= 10f * (weapons.ecoEnergy + 1))
                {
                    weapons.energy -= difficulty.energySpend;
                    timer = 0;
                    scoreManager.EnergyChange();
                }
                if (fixedJoystick.Vertical > 0 && variableJoystick.Vertical > 0) { trail.SetActive(true); speed = 7f; }
                else if (fixedJoystick.Vertical < 0 && variableJoystick.Vertical < 0) { trail.SetActive(true); speed = 7f; }
                else if (fixedJoystick.Horizontal < 0 && variableJoystick.Horizontal < 0) { trail.SetActive(true); speed = 7f; }
                else if (fixedJoystick.Horizontal > 0 && variableJoystick.Horizontal > 0) { trail.SetActive(true); speed = 7f; }
            }
            else trail.SetActive(false);
            if (variableJoystick.Vertical != 0f || variableJoystick.Horizontal != 0f)
            {
                weapons.shooting = true;
                rotation = (1 - variableJoystick.Vertical) * 90;
                if (variableJoystick.Horizontal < 0) rotation *= -1;
            }
            else weapons.shooting = false;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
        else if (weapons.weapon.iD == 4)
        {
            if (variableJoystick.Vertical != 0f || variableJoystick.Horizontal != 0f)
            {
                weapons.shooting = true;
                rotation = (1 - variableJoystick.Vertical) * 90;
                if (variableJoystick.Horizontal < 0) rotation *= -1;
            }
            else weapons.shooting = false;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
        else // Если нет энергии
        {
            trail.SetActive(false);
            if (!died)
            {
                spawnManager.SendMessage("NoEnergy");
                died = true;
            }
        }
    }

    
}

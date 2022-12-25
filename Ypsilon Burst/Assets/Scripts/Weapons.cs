using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapons : MonoBehaviour
{
    public int energy;
    [SerializeField] private int HP;
    private int maxHP;
    private int regenHP;
    private float armorRate;
    private int explosiveAmmoIndex;
    private int sturdyIndex;
    private int drillIndex;
    public int ecoEnergy;
    private float PlayerSpeed;
    private Rigidbody PlayerRb;
    private bool alive;

    private Transform shootingPoint;
    private GameObject Bullet;
    private int Damage;
    private float BulletSpeed;
    private float RateOfFire;
    private bool toHeal;
    public bool shooting;
    private float SCycle;
    private float timer;

    private int IncreasedHP, FasterRegen, Armor, ExplosiveAmmo, sturdy, drill;

    public Weapon weapon;
    private int energyTaken;

    private GameObject canvas, deathCanvas;

    private ScoreManager scoreManager;
    private SpawnManager spawnManager;

    [SerializeField] private GameObject[] parts1;
    [SerializeField] private GameObject[] parts2;
    [SerializeField] private GameObject explosion;
    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private AdsManager adsManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip hurtSound;
    private Difficulty difficulty;

    private AudioSource weaponSound;
    [SerializeField] private AudioClip laserSound;
    public void Awake()
    {
        deathCanvas = GameObject.Find("Death Canvas");
    }
    public void Start()
    {
        energyTaken = weapon.Energy;
        difficulty = GameObject.Find("DifficultyManager").GetComponent<Difficulty>();
        alive = true;
        Bullet = weapon.Bullet;
        Damage = weapon.Damage;
        BulletSpeed = weapon.BulletSpeed;
        RateOfFire = weapon.RateOfFire;
        SCycle = 0;
        shooting = false;
        PlayerRb = GetComponent<Rigidbody>();
        shootingPoint = GameObject.Find("ShootingPoint").GetComponent<Transform>();
        //// задать значения из класса
        canvas = GameObject.Find("Canvas");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        weaponSound = shootingPoint.GetComponent<AudioSource>();
        bonusText = GameObject.Find("Canvas/BonusText").GetComponent<TextMeshProUGUI>();
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        deathCanvas.SetActive(false);
        timer = 0f;
        armorRate = 1;
        regenHP = 10;
        maxHP = 100;
        sturdy = 1;
        drill = 1;
    }

    private void Update()
    {
        if (weaponSound == null) Start();
        if (Time.timeScale < 1f) Time.timeScale += Time.deltaTime / 10f;
        if (Time.timeScale > 1f) Time.timeScale = 1f;
            if (alive)
        {
            if (shooting)
            {
                SCycle += Time.deltaTime;
                if (SCycle >= RateOfFire)
                {
                    Shoot();
                    SCycle = 0;
                }
            }
            PlayerSpeed = PlayerRb.velocity.magnitude;
        }
        if (toHeal)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                HP += regenHP;
                timer = 0;
                if (HP >= maxHP)
                {
                    HP = maxHP;
                    toHeal = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid" & PlayerRb.velocity.magnitude >= 10f)
        {
            Damaged(System.Convert.ToInt32(PlayerRb.velocity.magnitude * difficulty.asteroidDamage / sturdy));
        }
    }

    private void Shoot()
    {
        if (energy >= energyTaken * difficulty.energySpend || weapon.iD == 4)
        {
            energy -= energyTaken * difficulty.energySpend;
            PlayWeaponSound(laserSound);
            GameObject instbullet = Instantiate(Bullet, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;
            Rigidbody instRB = instbullet.GetComponent<Rigidbody>();
            instRB.AddRelativeForce(new Vector3(0, 0, 1f + PlayerSpeed / 2) * BulletSpeed * Time.deltaTime);
            instRB.gameObject.SendMessage("SetDamage", Damage);
            if(explosiveAmmoIndex > 0) instRB.gameObject.SendMessage("ExplosiveAmmo", explosiveAmmoIndex);
            scoreManager.EnergyChange();
            if(drill>1) instRB.gameObject.SendMessage("Drill", drillIndex);
        }
    }

    public void ShootButton()
    {
        shooting = true;
        Shoot();
    }
    public void ShootButtonUp()
    {
        shooting = false;
        SCycle = 0;
    }
    private void Damaged(int damage)
    {
        weaponSound.volume = Random.Range(0.6f, 0.7f);
        weaponSound.pitch = Random.Range(0.7f, 0.8f);
        weaponSound.PlayOneShot(hurtSound);
        toHeal = false;
        HP -= System.Convert.ToInt32(damage/armorRate);
        if (HP <= 0f) Killed();
        timer = 0f;
        Invoke("Heal", 5f);
    }

    private void Killed() 
    {
        Instantiate(explosion, transform.position, transform.rotation);
        if (Random.Range(1, 3) == 1 || parts2.Length == 0) //// max is exclusive for int !!!!
        {
            for (int i = 0; i < parts1.Length; i++)
            {
                GameObject part = Instantiate(parts1[i], transform.position, transform.rotation);
                part.gameObject.SendMessage("SetMaterial", GetComponent<MeshRenderer>().material);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100000f * Time.deltaTime);
            }
        }
        else
        {
            for (int i = 0; i < parts2.Length; i++)
            {
                GameObject part = Instantiate(parts2[i], transform.position, transform.rotation);
                part.gameObject.SendMessage("SetMaterial", GetComponent<MeshRenderer>().material);
                part.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100000f * Time.deltaTime);
            }
        }
        alive = false;
        canvas.SetActive(false);
        deathCanvas.SetActive(true);
        GameObject.Find("ScoreManager").SendMessage("CheckScore");
        this.gameObject.SetActive(false);
        if (Random.Range(0f, 1f) >= 0.7f) adsManager.ShowAd();
    }

    private void Heal()
    {
        toHeal = true;
    }

    private void PowerUp(int index)
    {

    }
    private void IncreasedHPBonus()
    {
        IncreasedHP++;
        maxHP += 20;
        bonusText.text = "You picked up the Increased HP bonus!";
        PlaySound();
        Invoke("ClearText", 5f);
        toHeal = true;
    }
    private void FasterRegenBonus()
    {
        FasterRegen++;
        regenHP += 5;
        bonusText.text = "You picked up the Health Regeneration bonus!";
        PlaySound();
        Invoke("ClearText", 5f);
    }
    private void ArmorBonus()
    {
        Armor++;
        armorRate += 0.2f;
        bonusText.text = "You picked up the Armor bonus!";
        PlaySound();
        Invoke("ClearText", 5f);
    }
    private void ExplosiveAmmoBonus()
    {
        ExplosiveAmmo++;
        explosiveAmmoIndex++;
        bonusText.text = "You picked up the Explosive Ammo bonus!";
        PlaySound();
        Invoke("ClearText", 5f);
    }
    private void SturdyBonus()
    {
        sturdy *= 5;
        sturdyIndex++;
        bonusText.text = "You picked up the Sturdy bonus!";
        PlaySound();
        Invoke("ClearText", 5f);
    }
    private void DrillBonus()
    {
        drill++;
        drillIndex++;
        bonusText.text = "You picked up the Drill bonus!";
        PlaySound();
        Invoke("ClearText", 5f);
    }
    private void EcoEnergyBonus()
    {

        if (ecoEnergy == 0) 
        { 
            bonusText.text = "You picked up the Eco Energy bonus!";
            ecoEnergy = 1;
            energyTaken /= 2; 
        }
        else bonusText.text = "You already have the Eco Energy bonus!";
        PlaySound();
        
        Invoke("ClearText", 5f);
    }
    private void GunBotBonus()
    {
        bonusText.text = "You've got your own GunBot!";
        spawnManager.SendMessage("SpawnGunBot");
        PlaySound();
        Invoke("ClearText", 5f);
    }
    private void ClearText()
    {
        bonusText.text = "";
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying) 
        {
            audioSource.PlayOneShot(pickUpSound);
        }
    }
    private void PlayWeaponSound(AudioClip sound) 
    {
        if (!weaponSound.isPlaying)
        {
            weaponSound.volume = Random.Range(0.6f, 0.7f);
            weaponSound.pitch = Random.Range(0.7f, 0.8f);
            weaponSound.PlayOneShot(sound);
        }
    }
    private void Revive()
    {
        HP = maxHP;
        alive = true;
        canvas.SetActive(true);
        deathCanvas.SetActive(false);
        Debug.Log("2");
        this.gameObject.SetActive(true);
        energy += 100;
        //Time.timeScale = 0.5f;
    }
}

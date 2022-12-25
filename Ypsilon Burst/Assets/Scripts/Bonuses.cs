using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public PowerUp powerUp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (powerUp)
            {
                case PowerUp.IncreaseHP:
                    collision.gameObject.SendMessage("IncreasedHPBonus");
                    break;
                case PowerUp.FasterRegen:
                    collision.gameObject.SendMessage("FasterRegenBonus");
                    break;
                case PowerUp.Armor:
                    collision.gameObject.SendMessage("ArmorBonus");
                    break;
                case PowerUp.ExplosiveAmmo:
                    collision.gameObject.SendMessage("ExplosiveAmmoBonus");
                    break;
                case PowerUp.Sturdy:
                    collision.gameObject.SendMessage("SturdyBonus");
                    break;
                case PowerUp.Drill:
                    collision.gameObject.SendMessage("DrillBonus");
                    break;
                case PowerUp.EcoEnergy:
                    collision.gameObject.SendMessage("EcoEnergyBonus");
                    break;
                case PowerUp.GunBot:
                    collision.gameObject.SendMessage("GunBotBonus");
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
public enum PowerUp
{
    IncreaseHP,
    FasterRegen,
    Armor,
    ExplosiveAmmo,
    Sturdy,
    Drill,
    EcoEnergy,
    GunBot
}
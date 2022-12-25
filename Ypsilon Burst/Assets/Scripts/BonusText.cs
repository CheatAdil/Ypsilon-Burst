using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text.text = "";
    }

    private void PickedBonus(string bonusName)
    {
        Text.text = "You picked up the " + bonusName + "bonus!";
        Invoke("Start", 10f);
    }
}

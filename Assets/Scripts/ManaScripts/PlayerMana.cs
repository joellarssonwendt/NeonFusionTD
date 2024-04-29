using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{

    public int maxMana = 100;
    public int currentMana;

    public ManaBar manaBar;


    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseMana (int manacost)
    {
        currentMana -= manacost;

        manaBar.SetMana(currentMana);
    }

    public void RestoreMana()
    {
        currentMana = maxMana;
        manaBar.SetMana(currentMana);
    }

}

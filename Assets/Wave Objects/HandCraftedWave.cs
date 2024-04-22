using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "HandCraftedWave")]
public class HandCraftedWave : ScriptableObject
{
    // enemyTypes avg�r vilken ordning och vilken typ av fiende som skall spawnas
    // enemyAmounts avg�r hur m�nga av varje fiende som ska spawnas ur varje steg i enemyTypes
    // St�ll in dessa v�rden i inspectorn i Unity, l�gg sedan in ScriptableObjectet i LevelManagerns lista �ver HandCraftedWaves.

    // Exempel nedan:
    // enemyTypes = basic, small, basic, large, basic
    // enemyAmounts = 1, 2, 2, 1, 1
    // Detta kommer resultera i en Wave som best�r av: basic, small, small, basic, basic, large, basic

    public GameObject[] enemyTypes;
    public int[] enemyAmounts;
}

using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "HandCraftedWave")]
public class HandCraftedWave : ScriptableObject
{
    // enemyTypes avgör vilken ordning och vilken typ av fiende som skall spawnas
    // enemyAmounts avgör hur många av varje fiende som ska spawnas ur varje steg i enemyTypes
    // Ställ in dessa värden i inspectorn i Unity, lägg sedan in ScriptableObjectet i LevelManagerns lista över HandCraftedWaves.

    // Exempel nedan:
    // enemyTypes = basic, small, basic, large, basic
    // enemyAmounts = 1, 2, 2, 1, 1
    // Detta kommer resultera i en Wave som består av: basic, small, small, basic, basic, large, basic

    public GameObject[] enemyTypes;
    public int[] enemyAmounts;
}

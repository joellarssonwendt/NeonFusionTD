using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "HandCraftedWave")]
public class HandCraftedWave : ScriptableObject
{
    // enemyTypes avg�r vilken ordning och vilken typ av fiende som skall spawnas
    // enemyAmounts avg�r hur m�nga av varje fiende som ska spawnas ur varje steg i enemyTypes
    // enemiesPerSecond avg�r hur m�nga fiender som kommer spawna per sekund
    // St�ll in dessa v�rden i inspectorn i Unity, l�gg sedan in ScriptableObjectet i LevelManagerns lista �ver HandCraftedWaves.

    // Exempel nedan:
    // enemyTypes = basic, small, basic, large, basic
    // enemyAmounts = 1, 2, 2, 1, 1
    // Detta kommer resultera i en Wave som best�r av: basic, small, small, basic, basic, large, basic

    public List<GameObject> enemyTypes;
    public int[] enemyAmounts;
    public float enemiesPerSecond;
}

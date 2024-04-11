using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform spawnPoint;
    public Transform[] pathingNodes;

    private void Awake()
    {
        main = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject gourdPrefab;
    [SerializeField]
    private GameObject pumpkingPrefab;
    [SerializeField]
    private GameObject squashPrefab;

    [SerializeField]
    private float gourdInterval = 5f;
    [SerializeField]
    private float pumpkingInterval = 10f;
    [SerializeField]
    private float squashInterval = 7f;

    [SerializeField]
    private float xMin = -30f;
    [SerializeField]
    private float xMax = 30f;
    [SerializeField]
    private float zMin = -30f;
    [SerializeField]
    private float zMax = 30f;

    public GameObject player;

    public static int mobAmount = 0;
    public int mobAmountMax = 100;

    private bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(pumpkingInterval, pumpkingPrefab));
        StartCoroutine(spawnEnemy(squashInterval, squashPrefab));
        StartCoroutine(spawnEnemy(gourdInterval, gourdPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.vRGamePaused)
        {
            gamePaused = true;
            return;
        }
            
        if (gamePaused)
        {
            StartCoroutine(spawnEnemy(pumpkingInterval, pumpkingPrefab));
            StartCoroutine(spawnEnemy(squashInterval, squashPrefab));
            StartCoroutine(spawnEnemy(gourdInterval, gourdPrefab));
            gamePaused = false;
        }
        
    }

    public IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if (!GlobalVariables.vRGamePaused)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(player.transform.position.x + Random.Range(xMin, xMax), 0.599f, player.transform.position.z + Random.Range(zMin, zMax)), Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
            mobAmount += 1;
        }
    }
}

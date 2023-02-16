using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] metor;
    [SerializeField] private GameObject rabbitPrefab;
    [SerializeField] private Transform rabbitParent;
    [SerializeField] private Transform obsParent;
    [SerializeField] private Transform[] spawnPoint, spawnPointRabbit;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameOverPopup gameOverPopup;
    public static GameManager instance;
    public int count;
    public static float time;

    void Awake()
    {
        instance = this;
        count = 0;
    }
    void Start()
    {
        InvokeRepeating("InstiateObs", 7, 3);
    }

    public void InstiateObs()
    {
        var randomSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
        var randomMetor = metor[Random.Range(0, metor.Length)];
        var obsIns = Instantiate(randomMetor);
        obsIns.transform.position = randomSpawnPoint.transform.position;
        obsIns.transform.SetParent(obsParent);
    }

    public void InstiateRabbit()
    {
        var randomSpawnPoint = spawnPointRabbit[Random.Range(0, spawnPointRabbit.Length)];
        var pos = randomSpawnPoint.transform.position;
        var rot = randomSpawnPoint.transform.rotation;
        var obsIns = Instantiate(rabbitPrefab, pos, rot);
    }


    public void GameOver()
    {
        UIManager.EnableGameOverPopUp(true);
        gameOverPopup.LoadText();
        ScoreManager.instance.SaveHighScore();
        UIManager.EnableMainGame(false);
        UIManager.EnableEnviroment(false);
        CancelInvoke();
    }
}

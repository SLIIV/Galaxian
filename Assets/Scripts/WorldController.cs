using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    /// <summary>
    /// Трансформ главного фона
    /// </summary>
    public GameObject background;

    /// <summary>
    /// Коллайдеры по бокам
    /// </summary>
    private BoxCollider2D[] borders;

    /// <summary>
    /// Префаб астеройда
    /// </summary>
    public GameObject[] asteroidPrefab;

    /// <summary>
    /// Текущий уровень
    /// </summary>
    public int curLevel;

    /// <summary>
    /// Минимальная задержка спавна астероида
    /// </summary>
    public float minDelayToSpawn;

    /// <summary>
    /// Максимальная задержка спавна астероида
    /// </summary>
    public float maxDelayToSpawn;

    /// <summary>
    /// Количество астероидов для победы
    /// </summary>
    public int asteroidsToWin;

    /// <summary>
    /// Текущее количество астероидов уничтожено
    /// </summary>
    public int curAsteroidsEluminated;

    /// <summary>
    /// Состояние всех уровней
    /// </summary>
    public LevelState[] levelStates;

    /// <summary>
    /// Окно попебы
    /// </summary>
    public GameObject winWindow;

    /// <summary>
    /// Айди следующего уровня
    /// </summary>
    public int nextLevelId;

    /// <summary>
    /// Начальное количество астероидов
    /// </summary>
    private int startAsteroids;

    public AudioManager audioManager;

    public GameObject timerParent;

    public Text timer;

    public float timeToWin;

    public bool canFire;

    private void Start()
    {
        //Настраиваем уровень
        minDelayToSpawn =　levelStates[curLevel].MinAsteroidSpawnDelay;
        maxDelayToSpawn = levelStates[curLevel].MaxAsteroidSpawnDelay;
        asteroidsToWin = levelStates[curLevel].AsteroidsToWin;
        startAsteroids = levelStates[curLevel].StartAsteroids;
        canFire = levelStates[curLevel].CanFire;
        if(levelStates[curLevel].IsTimerLevel)
        {
            timerParent.SetActive(true);
            timeToWin = levelStates[curLevel].TimeToWin;
            timer.text = timeToWin.ToString();
            StartCoroutine(StartTimer());
        }
        
        if(audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();
        }

        borders = background.GetComponentsInChildren<BoxCollider2D>();

        for(int i = 0; i < borders.Length; i++)
        {
            //Устанавливаем размер коллайдеров относительно экрана
            if(borders[i].gameObject.layer == LayerMask.NameToLayer("HorBord"))
            {
                borders[i].size = new Vector2(borders[i].size.x, Screen.width * 2);
            }
            else if(borders[i].gameObject.layer == LayerMask.NameToLayer("VertBord"))
            {
                borders[i].size = new Vector2(borders[i].size.x, Screen.height * 2);
            }
        }
        nextLevelId = curLevel + 1;

        Init(startAsteroids);
        
    }
    private void Update()
    {
        //Обрабатываем конец уровня
        if(levelStates[curLevel].IsTimerLevel && timeToWin == 0)
        {
            LevelComplete();
        }
        else if(curAsteroidsEluminated >= asteroidsToWin && !levelStates[curLevel].IsTimerLevel)
        {
            LevelComplete();
        }
    }
    
    /// <summary>
    /// Инициализация уровня
    /// </summary>
    /// <param name="count"></param>
    public void Init(int count)
    {
        for(int i = 0; i < count; i++)
        {
            CreateAsteroid();
        }
    }

    /// <summary>
    /// Создаёт астероид
    /// </summary>
    public void CreateAsteroid()
    {
        StartCoroutine(CreateAsteroidByDelay(minDelayToSpawn, maxDelayToSpawn));
    }

    /// <summary>
    /// Создаёт астероид с задержкой
    /// </summary>
    /// <param name="minDelay">Минимальная задержка</param>
    /// <param name="maxDelay">Максильная задержка</param>
    /// <returns></returns>
    private IEnumerator CreateAsteroidByDelay(float minDelay, float maxDelay)
    {
        float delay = Random.Range(minDelay, maxDelay);
        int randSprite = Random.Range(0, asteroidPrefab.Length);
        yield return new WaitForSeconds(delay);
        //Получаем точку спавна по всей оси X, равной ширине экрана
        Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height * 1.1f, 10));
        Instantiate(asteroidPrefab[randSprite], spawnPoint, Quaternion.identity, transform);
    }

    /// <summary>
    /// Обрабатывает конец уровня
    /// </summary>
    private void LevelComplete()
    {
        //Меняем состояние уровней
        levelStates[curLevel].State = "Passed";
        levelStates[curLevel].SaveData();

        winWindow.SetActive(true);

        Time.timeScale = 0;
        curAsteroidsEluminated = 0;

        if(curLevel + 1 < levelStates.Length && levelStates[nextLevelId].State != "Passed")
        {
            levelStates[nextLevelId].State = "Not passed";
            levelStates[nextLevelId].SaveData();
        }

        audioManager.PlaySound(0);
    }

    private IEnumerator StartTimer()
    {
        while(timeToWin > 0)
        {
            yield return new WaitForSeconds(1);
            timeToWin--;
            timer.text = timeToWin.ToString();
        }
    }
}

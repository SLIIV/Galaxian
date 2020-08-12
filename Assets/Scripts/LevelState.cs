using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelState", menuName = "ScriptableObjects/LevelState", order = 1)]
public class LevelState : ScriptableObject
{
    /// <summary>
    /// Айди уровня
    /// </summary>
    [SerializeField] private int id;
    /// <summary>
    /// Состояние уровня (Пройдено/Закрыто/Не пройдено)
    /// </summary>
    [SerializeField] private string state;

    /// <summary>
    /// Минимальная задержка между спавном асетеройдов
    /// </summary>
    [SerializeField] private float minAsteroidsSpawnDelay;

    /// <summary>
    /// Максимальная задержка между спавном астеройдов
    /// </summary>
    [SerializeField] private float maxAsteroidsSpawnDelay;

    /// <summary>
    /// Количество астеройдов, которое нужно уничтожить
    /// </summary>
    [SerializeField] private int asteroidsToWin;

    /// <summary>
    /// Начальное количество астеройдов на уровне
    /// </summary>
    [SerializeField] private int startAsteroids;


    /// <summary>
    /// Это уровень с таймером?
    /// </summary>
    [SerializeField] private bool isTimerlevel;

    [SerializeField] private float timeToWin;


    [SerializeField] private bool canFire;



    #region Поля
    public int Id
    {
        get
        {
            return id;
        }
    }
    public string State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    public float MinAsteroidSpawnDelay
    {
        get
        {
            return minAsteroidsSpawnDelay;
        }
    }
    public float MaxAsteroidSpawnDelay
    {
        get
        {
            return maxAsteroidsSpawnDelay;
        }
    }
    public int AsteroidsToWin
    {
        get
        {
            return asteroidsToWin;
        }
    } 
    public int StartAsteroids
    {
        get
        {
            return startAsteroids;
        }
    }

    public bool IsTimerLevel
    {
        get
        {
            return isTimerlevel;
        }
    }
    public float TimeToWin
    {
        get
        {
            return timeToWin;
        }
    }
    public bool CanFire
    {
        get
        {
            return canFire;
        }
    }
    #endregion

    /// <summary>
    /// Сохраняет состояние текущего уровня
    /// </summary>
    public void SaveData()
    {
        PlayerPrefs.SetString($"levelState{Id}", state);
        PlayerPrefs.Save();
    }

}

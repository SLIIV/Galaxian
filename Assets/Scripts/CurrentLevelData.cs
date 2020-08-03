using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLevelData : MonoBehaviour
{

    /// <summary>
    /// информация о текущем уровне
    /// </summary>
    public LevelState levelData;

    /// <summary>
    /// Текст с состоянием уровня
    /// </summary>
    public Text state;

    /// <summary>
    /// Кнопка запуска уровня
    /// </summary>
    public Button levelButton;


    private void Start()
    {
        //получаем состояние уровней
        if (levelData.Id != 1)
        {
            levelData.State = PlayerPrefs.GetString($"levelState{levelData.Id}", "Закрыт");
        }
        else
        {
            levelData.State = PlayerPrefs.GetString($"levelState{levelData.Id}", "Не пройдено");
        }

        //Закрываем не открытые уровни
        state.text = levelData.State;
        if(levelData.State == "Закрыт")
        {
            levelButton.interactable = false;
        }
    }
}

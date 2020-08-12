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
            levelData.State = PlayerPrefs.GetString($"levelState{levelData.Id}", "Closed");
        }
        else
        {
            levelData.State = PlayerPrefs.GetString($"levelState{levelData.Id}", "Not passed");
        }

        //Закрываем не открытые уровни
        state.text = levelData.State;
        if(levelData.State == "Closed")
        {
            levelButton.interactable = false;
        }
        else
        {
            levelButton.interactable = true;
        }
    }
}

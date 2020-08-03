using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    /// <summary>
    /// Скрипт управляющий МИРОМ
    /// </summary>
    public WorldController worldController;

    /// <summary>
    /// Объект с игрой
    /// </summary>
    public GameObject game;

    /// <summary>
    /// Обработка нажатия на уровень
    /// </summary>
    /// <param name="button"></param>
    public void OnLevelClick(GameObject button)
    {
        //Передаём данные в контроллер и запускаем игру
        worldController.curLevel = button.GetComponent<CurrentLevelData>().levelData.Id;
        worldController.curLevel -= 1;
        gameObject.SetActive(false);
        game.SetActive(true);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}

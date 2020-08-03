using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Текст с количеством жизней
    /// </summary>
    public Text livesCount;

    /// <summary>
    /// Контроллер игрока
    /// </summary>
    public PlayerController player;

    private void OnGUI()
    {
        //Обновляем интерфейс
        livesCount.text = player.health.ToString();
    }

    /// <summary>
    /// Выход в главное меню
    /// </summary>
    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


   
}

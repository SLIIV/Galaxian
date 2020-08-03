using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    /// <summary>
    /// Кэшированный компонент
    /// </summary>
    private Transform transform;

    /// <summary>
    /// Скорость астеройда
    /// </summary>
    public float speed;

    /// <summary>
    /// Игровой контроллер
    /// </summary>
    private WorldController worldController;
    void Start()
    {
        //Получаем нужные компоненты
        transform = GetComponent<Transform>();
        worldController = GameObject.FindGameObjectWithTag("GameController").GetComponent<WorldController>();
    }

    
    void FixedUpdate()
    {
        //Двигаем астероид в сторону игрока
        transform.position += Vector3.down * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Обрабатываем столкновение со стеной
        if (collision.CompareTag("DeleteAsteroid"))
        {
            Destroy(gameObject, 1);
        }
    }

    //Создаём новый астероид после уничтожения этого
    private void OnDestroy()
    {
        if(worldController.isActiveAndEnabled)
            worldController.CreateAsteroid();
    }


}

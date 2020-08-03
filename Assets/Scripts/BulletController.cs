using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    /// <summary>
    /// Скорость пули
    /// </summary>
    public float speed;


    /// <summary>
    /// кэшированный компонент
    /// </summary>
    private Transform transform;

    /// <summary>
    /// Скрипт управляющий игрой
    /// </summary>
    private WorldController world;
    void Start()
    {
        //Получаем компоненты
        transform = GetComponent<Transform>();
        world = GameObject.FindGameObjectWithTag("GameController").GetComponent<WorldController>();
    }

    void FixedUpdate()
    {
        //Двигаем пулю прямо вверх
        transform.position += Vector3.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Обрабатывем столкновение с астеройдом
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
            world.curAsteroidsEluminated++;
            world.audioManager.PlaySound(2);
        }
    }
}

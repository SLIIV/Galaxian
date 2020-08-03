using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

/// <summary>
/// Скрипт управляющий игроком
/// </summary>
public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// Скорость
    /// </summary>
    public float speed;

    public Vector2 spawnPoint;

    /// <summary>
    /// кэшированный компонент трансформа игрока
    /// </summary>
    private Transform playerTransform;

    /// <summary>
    /// Направление перемещения по x
    /// </summary>
    private float xAxis;


    /// <summary>
    /// Направление перемещения по y
    /// </summary>
    private float yAxis;

    /// <summary>
    /// Скрипт управления джойстиком
    /// </summary>
    public Joystick joystick;

    /// <summary>
    /// Префаб пули
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Количество жизний игрока
    /// </summary>
    public int health;

    /// <summary>
    /// Проверка на смерть игрока
    /// </summary>
    public bool isDead;

    /// <summary>
    /// Окно с проигрешем
    /// </summary>
    public GameObject loseWindow;

    /// <summary>
    /// Проверка на перезарядку
    /// </summary>
    private bool isReloading;

    /// <summary>
    /// Компонент аудио
    /// </summary>
    public AudioManager audioManager;


    void Start()
    {
        //Получаем компоненты и настраиваем окружение
        playerTransform = GetComponent<Transform>();

        playerTransform.position = spawnPoint;

        Time.timeScale = 1;
    }

    
    void Update()
    {
        
        if (isDead)
            return; //если мерты - останавливаем игру

        //Получаем данные о нажатиях
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        //Двигаем игрока в определённую сторону
        playerTransform.position += new Vector3(xAxis, yAxis) * Time.deltaTime * speed;
        playerTransform.position += new Vector3(joystick.Horizontal, joystick.Vertical) * Time.deltaTime * speed;
    }

    /// <summary>
    /// Обработка выстрела
    /// </summary>
    private void Fire()
    {
        if (!isReloading)
        {
            //Создаём пульку
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            Destroy(bullet, 5);
            isReloading = true;
            StartCoroutine(WeaponReload(0.5f));
            audioManager.PlaySound(3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Обрабатываем столкновение с астеройдом
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Destroy(collision.collider.gameObject);
            health--;
            if(health <= 0)
            {
                Death();
            }
            else
                audioManager.PlaySound(2);
        }
    }

    /// <summary>
    /// Обработка смерти игрока
    /// </summary>
    private void Death()
    {
        isDead = true;
        loseWindow.SetActive(true);
        audioManager.PlaySound(1);
        Time.timeScale = 0;
        
    }
    
    /// <summary>
    /// Перезарядка оружия
    /// </summary>
    /// <param name="delay">Скорость перезарядки</param>
    /// <returns></returns>
    private IEnumerator WeaponReload(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReloading = false;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;
using DG.Tweening;

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
    [HideInInspector] public bool isReloading;

    /// <summary>
    /// Компонент аудио
    /// </summary>
    public AudioManager audioManager;


    /// <summary>
    /// Проверка, летели ли мы вправо
    /// </summary>
    private bool isRight = false;

    /// <summary>
    /// Проверка, летели ли мы влево
    /// </summary>
    private bool isLeft = false;

    private Animator animator;

    public WorldController world;

    private Transform camera;


    void Start()
    {
        //Получаем компоненты и настраиваем окружение
        playerTransform = GetComponent<Transform>();

        animator = GetComponent<Animator>();

        playerTransform.position = spawnPoint;

        Time.timeScale = 1;

        if(audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();
        }
        camera = Camera.main.transform;

    }

    
    void Update()
    {
        
        if (isDead)
            return; //если мертвы - останавливаем игру

        //Получаем данные о нажатиях
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Fire();
        //}
    }

    void FixedUpdate()
    {
        //Двигаем игрока в определённую сторону
        playerTransform.position += new Vector3(xAxis, yAxis) * Time.deltaTime * speed;
    }

    /// <summary>
    /// Обработка выстрела
    /// </summary>
    public void Fire()
    {
        if (!isReloading && world.canFire)
        {
            //Создаём пульку
            GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.up, Quaternion.Euler(0, 0, -90));
            Destroy(bullet, 5);
            isReloading = true;
            StartCoroutine(WeaponReload(0.75f));
            audioManager.PlaySound(3);
            camera.DOShakePosition(0.2f, 0.25f, 50);
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
            camera.DOShakePosition(0.5f, 0.5f, 15);
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

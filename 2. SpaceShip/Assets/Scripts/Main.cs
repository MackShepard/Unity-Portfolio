using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S; // Объект-одиночка Main
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;

    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;


    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[]
    { 
        WeaponType.blaster, WeaponType.blaster,
        WeaponType.spread, WeaponType.shield
    };

    public void ShipDestroyed(Enemy e)
    { // c
      // Сгенерировать бонус с заданной вероятностью
        if (Random.value <= e.powerUpDropChance)
        { // d
          // Выбрать тип бонуса
          // Выбрать один из элементов в powerUpFrequency
            int ndx = Random.Range(0, powerUpFrequency.Length); // e
            WeaponType puType = powerUpFrequency[ndx];
            // Создать экземпляр PowerUp
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            // Установить соответствующий тип WeaponType
            pu.SetType(puType); // f
            pu.transform.position = e.transform.position;
        }            
    }

    private void Start()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        // Вызывать SpawnEnemy() один раз (в 2 секунды при значениях по умолчанию)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
        // Словарь с ключами типа WeaponType
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>(); // a
        foreach (WeaponDefinition def in weaponDefinitions)
        { // b
            WEAP_DICT[def.type] = def;
        }
    }
    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length); 
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]); 
                                                                     
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        { 
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
       
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;
 
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond ); // g
}
    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    /// <summary>
    /// Статическая функция, возвращающая WeaponDefinition из статического
    /// защищенного поля WEAP_DICT класса Main.
    ///</summary>
    ///<returns>Экземпляр WeaponDefinition или, если нет такого определения
    /// для указанного WeaponType, возвращает новый экземпляр WeaponDefinition
    /// с типом none.</returns>
    ///<param name = "wt" > Tип оружия WeaponType, для которого требуется получить
    /// WeaponDefinition</param>
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    { //a
      // Проверить наличие указанного ключа в словаре
      // Попытка извлечь значение по отсутствующему ключу вызовет ошибку,
      // поэтому следующая инструкция играет важную роль.
        if (WEAP_DICT.ContainsKey(wt))
        { // b
            return (WEAP_DICT[wt]);
        }
        // Следующая инструкция возвращает новый экземпляр WeaponDefinition
        // с типом оружия WeaponType.попе, что означает неудачную попытку
        // найти требуемое определение WeaponDefinition
        return (new WeaponDefinition()); //с
    }
}



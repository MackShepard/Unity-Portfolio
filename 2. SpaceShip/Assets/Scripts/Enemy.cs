using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Назначьте в инспекторе")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    public float showDamageDuration = 0.1f;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage;
    public float damageDoneTime;
    public bool notifiedOfDestruction;

    public float powerUpDropChance = 1f;
    protected BoundsCheck bndCheck;
    void Awake()
    { // b
        bndCheck = GetComponent<BoundsCheck>();
        // Получить материалы и цвет этого игрового объекта и его потомков
        materials = Utils.GetAllMaterials(gameObject); // b
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }

    }

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

    private void Update()
    {
        if (showingDamage && Time.time > damageDoneTime)
        { // c
            UnShowDamage();
        }
        Move();
        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero": // b
                Projectile p = otherGO.GetComponent<Projectile>();
                // Если вражеский корабль за границами экрана,
                // не наносить ему повреждений.
                if (!bndCheck.isOnScreen)
                { //с
                    Destroy(otherGO);
                    break;
                }
                // Поразить вражеский корабль
                ShowDamage();
                // Получить разрушающую силу из WEAP_DICT в классе Main,
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                { // d
                  // Уничтожить этот вражеский корабль
                    if (!notifiedOfDestruction){
                        Main.S.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    Destroy(this.gameObject);
                }
                Destroy(otherGO); // e
                
                break;
            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name); // f
                break;

        }

    }
    void ShowDamage()
    { // е
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }
    void UnShowDamage()
    { // f
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerBasicDamage : MonoBehaviour
{
    public enum DamageTypeEnum
    {
        physical
    }

    [SerializeField]
    ProgressHandler[] healthMonitors;


    [SerializeField]
    //Max HP för karaktärerna, eftersom att damagen är 10 dör dem efter 10 slag.
    float maxHealth = 100;

    float health;

    float Health
    {
        set
        {
            //Health är antingen en siffra 
            health = Mathf.Clamp(value, 0, maxHealth);
            
            foreach(ProgressHandler handler in healthMonitors)
            {
                handler.UpdateValues(health, maxHealth);
            }

            CheckDeath();

        }
        get
        {
            return health;
        }
    }



    private void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HashSet<Collider2D> colliders = new HashSet<Collider2D>();
        
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contactPoint = collision.GetContact(i);

            if (contactPoint.collider.tag == "Punch" || contactPoint.collider.tag == "Kick")
            {
                DamageDealer damageDealer = contactPoint.collider.GetComponent<DamageDealer>();

                if (damageDealer != null && !colliders.Contains(contactPoint.collider))
                {
                    float damage = damageDealer.DamageAmount;
                    Health -= damage;
                }

                colliders.Add(contactPoint.collider);

            }

        }

    }
    //En metod som kollar ifall någon av karaktärerna är döda. Ifall 
    private void CheckDeath()
    {
        if (Health <= 0)
        {
            GameStateManager.winnerName = GetComponent<PlayerController>().Opponent.FighterName;
            SceneManager.LoadScene("GameOver");
        }
    }

}

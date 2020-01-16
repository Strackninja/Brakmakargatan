using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //Hur mycket damage karaktärerna dealar
    [SerializeField]
    float damageAmount = 10;

    [SerializeField]
    PlayerBasicDamage.DamageTypeEnum damageType = PlayerBasicDamage.DamageTypeEnum.physical;

    public float DamageAmount
    {
        private set
        {
            damageAmount = value;
        }
        get
        {
            return damageAmount;
        }
    }

    public PlayerBasicDamage.DamageTypeEnum DamageType
    {
        private set
        {
            damageType = value;
        }
        get
        {
            return damageType;
        }
    }

}

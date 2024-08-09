using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public float maxHp { get; } = 100f;
    public float maxSp { get; } = 100f;
    public float curHp { get; set; }
    public float curSp { get; set; }
    public float meleeDamage { get; } = 10f;
    public bool isFalling { get; set; }
    public bool isAiming { get; set; }
    public PlayerModel()
    {
        curHp = maxHp;
        curSp = maxSp;
        isFalling = false;
        isAiming = false;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerModel _player = new PlayerModel();
    GameObject _mainCam;
    GameObject _aimCam;
    // Start is called before the first frame update
    void Start()
    {
        _mainCam =  GameObject.Find("MainCamera");
        _aimCam = GameObject.Find("AimCamera");
        _aimCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shoot();
    }
    public void GetDamage(float damage)
    {
        _player.curHp -= damage;
        if(_player.curHp < 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

    public void UseSp(float spAmount)
    {
        _player.curSp -= spAmount;
    }

    public void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            _mainCam.SetActive(false);
            _aimCam.SetActive(true);
            _player.isAiming = true;
        }
        else
        {
            _mainCam.SetActive(true);
            _aimCam.SetActive(false);
            _player.isAiming = false;
        }
    }

    public void Shoot()
    {
        if(_player.isAiming && Input.GetMouseButton(0)) {
        
        }
    }
    public void MeleeAttack()
    {
        if(!_player.isAiming && Input.GetMouseButton(0))
        {

        }
    }

}

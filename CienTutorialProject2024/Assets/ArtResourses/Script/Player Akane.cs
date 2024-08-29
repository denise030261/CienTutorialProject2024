using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAkane : MonoBehaviour
{
    //플레이어 무기관련 배열 함수 2개 선언
    public GameObject[] weapons;
    public bool[] hasWeapons;

    //아이템 먹기
    GameObject nearObject;
    bool iDown;

    //아이템 교체
    Weapon equipWeapon;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    int equipWeaponIndex = -1;
    public List<GameObject> GunSelect = new List<GameObject>();

    //공격(키입력)
    bool fDown;
    float fireDelay;
    bool isFireReady = true;

    Vector3 moveVec;

    //보호막 관련
    public GameObject life2;

    //애니메이션
    Animator _animator;
    public Transform model;

    void Awake()
    {
        _animator = model.GetComponent<Animator>();
        hasWeapons = new bool[3];
    }

    void FixedUpdate()
    {

    }
    void Update()
    {
        GetInput();
        Swap();
        Interaction();
        Attack();
        Shield();

    }

    void GetInput()
    {

        iDown = Input.GetButtonDown("Interaction");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        fDown = Input.GetButtonDown("Fire1");

    }



    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (fDown && isFireReady)
        {
            equipWeapon.Use();
            _animator.SetTrigger("doShot");
            fireDelay = 0;
        }
    }

    void Swap()
    {
        //무기 획득 안했거나 같은 무기가지고 있을때는 swap 못하게끔
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if (sDown1 || sDown2 || sDown3)
        {
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
                GunSelect[1].SetActive(false);
                GunSelect[2].SetActive(false);
            }
            

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            GunChoice(weaponIndex);
            equipWeapon.gameObject.SetActive(true);
        }
    }

    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Debug.Log("get Weapon");
                //무기를 가지고 있는지
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                //아이템 정보 가져와서 해당 무기 입수 체크
                hasWeapons[weaponIndex] = true;

                //nearObject는 사라져야함
                Destroy(nearObject);
            }
        }
    }

    //무기 선택표시 관련
    void GunChoice(int num)
    {
        GunSelect[num].SetActive(true);
    }

    //보호막 관련
    void Shield()
    {
        bool have = false;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Shield"))
            {
                have = true;
                life2.SetActive(true);
            }
            else if (!have)
            {
                life2.SetActive(false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
}

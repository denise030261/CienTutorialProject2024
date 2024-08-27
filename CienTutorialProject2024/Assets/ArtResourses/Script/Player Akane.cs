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

    //공격(키입력)
    bool fDown;
    float fireDelay;
    bool isFireReady = true;

    Vector3 moveVec;


    //애니메이션
    Animator anim;

    void Awake()
    {
        hasWeapons = new bool[3];
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("doShot");
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
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
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



    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;

        }
        Debug.Log(nearObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
}

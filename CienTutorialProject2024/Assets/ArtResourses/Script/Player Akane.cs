using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAkane : MonoBehaviour
{
    // �ӵ�
    public float speed;
    //�÷��̾� ������� �迭 �Լ� 2�� ����
    public GameObject[] weapons;
    public bool[] hasWeapons;
    //������ ��ǥ
    float hAxis;
    float vAxis;
    
    //���� bool ����
    bool jDown;
    //�������� ���� ����
    bool isJump;

    //������ �Ա�
    GameObject nearObject;
    bool iDown;

    //������ ��ü
    Weapon equipWeapon;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    int equipWeaponIndex = -1;

    //����(Ű�Է�)
    bool fDown;
    float fireDelay;
    bool isFireReady = true;

    Vector3 moveVec;

    //����ȿ�� ����
    Rigidbody rigid;

    //�ִϸ��̼�
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void Update()
    {

        GetInput();
        Move();
        Turn();
        Jump();
        Swap();
        Interaction();
        Attack();
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        

        jDown = Input.GetButtonDown("Jump");

        iDown = Input.GetButtonDown("Interaction");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        fDown = Input.GetButtonDown("Fire1");
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        

        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
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
        //���� ȹ�� ���߰ų� ���� ���Ⱑ���� �������� swap ���ϰԲ�
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

        if ((sDown1 || sDown2 || sDown3) && !isJump)
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
        if (iDown && nearObject != null && !isJump)
        {
            if (nearObject.tag == "Weapon")
            {
                //���⸦ ������ �ִ���
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                //������ ���� �����ͼ� �ش� ���� �Լ� üũ
                hasWeapons[weaponIndex] = true;

                //nearObject�� ���������
                Destroy(nearObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
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

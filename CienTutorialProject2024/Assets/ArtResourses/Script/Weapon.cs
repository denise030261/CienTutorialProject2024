using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEfeect;

    //prefab�� ������ ��ġ
    public Transform bulletPos;
    public GameObject bullet;

    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if (type == Type.Range)
        {
            StartCoroutine(Shot());
        }


        IEnumerator Shot()
        {
            //#1. �Ѿ� �߻�
            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPos.forward * 10;

            yield return null;
            //#2. ź�� �߻�
            GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
            Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
            Vector3 caseVec = bulletCasePos.forward * Random.Range(-2, -1) + Vector3.up * Random.Range(1, 2);
            caseRigid.AddForce(caseVec, ForceMode.Impulse);
            caseRigid.AddTorque(Vector3.up * 2, ForceMode.Impulse);
        }
    }
}

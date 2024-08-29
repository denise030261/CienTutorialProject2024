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

    //prefab을 저장할 위치
    public Transform bulletPos;
    public GameObject bullet;

    public Transform bulletCasePos;
    public GameObject bulletCase;

    public Camera aimCam;

    public void Use()
    {
        if (type == Type.Range)
        {
            StartCoroutine(Shot());
        }


        IEnumerator Shot()
        {
            //#1. 총알 발사
            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
            Ray ray = aimCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 targetPoint;
            if(Physics.Raycast(ray, out RaycastHit hitInfo, 100, 1 << LayerMask.NameToLayer("Enemy")))
            {
                targetPoint = hitInfo.point;
                Debug.Log(hitInfo.collider.name);
            }
            else
            {
                targetPoint = ray.GetPoint(1000);
            }
            Vector3 bulletDir = (targetPoint - bulletPos.position).normalized;
            bulletRigid.velocity = bulletDir * 10;

            yield return null;
            //#2. 탄피 발사
            GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
            Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
            Vector3 caseVec = bulletCasePos.forward * Random.Range(-2, -1) + Vector3.up * Random.Range(1, 2);
            caseRigid.AddForce(caseVec, ForceMode.Impulse);
            caseRigid.AddTorque(Vector3.up * 2, ForceMode.Impulse);
        }
    }
}

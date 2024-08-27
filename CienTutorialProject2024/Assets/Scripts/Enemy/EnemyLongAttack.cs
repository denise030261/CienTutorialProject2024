using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject shoot;
    [SerializeField] float reloadTime = 1f;
    public GameObject projectileObject;
    private EnemyProjectile projectile;

    public bool isReload = true;
    public bool isDist = false;
    public bool isShoot = false;
    public Vector3 shootRotation;

    private void Update()
    {
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    public IEnumerator LongAttack()
    {
        Debug.Log("�߻�");

        // Ÿ���� ���� ���� ���
        Vector3 direction = target.transform.position - transform.position;
        Vector3 shootRotation = direction.normalized;

        // �� ��� �ִϸ��̼� ��� �ð�
        yield return new WaitForSeconds(0.833f);

        GameObject projectileInstance = Instantiate(projectileObject, shoot.transform.position, Quaternion.LookRotation(shootRotation));

        projectile = projectileInstance.GetComponent<EnemyProjectile>();
        projectile.targetDir = shootRotation;

        Debug.Log(projectile.targetDir);

        isShoot = false;

        // ������ ��� �ð�
        yield return new WaitForSeconds(reloadTime);
        isReload = true;
    }
}

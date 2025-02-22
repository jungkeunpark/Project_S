using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    // { NPC에 따라
    // 플레이어 감지 지역
    public GameObject trigger;
    // 퀘스트
    // 아직 안씀
    // 대사
    [SerializeField] DialogTable dialogTable;
    public List<string> dialogs;
    // } NPC에 따라

    private void Awake()
    {
        dialogs = new List<string>();

        for (int i = 0; i < dialogTable.dataArray.Length; i++)
        {
            if (dialogTable.dataArray[i].ID == 0)
            {
                dialogs.Add(dialogTable.dataArray[i].Dialog);
            }

            //Debug.Log(choiceDialog[i]);
        }
    }

    [HideInInspector]
    public float viewAngle = 90f; // 시야각 설정
    [HideInInspector]
    public float viewRadius = 2f; // 시야 반경 설정

    public void DetectPlayer()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, 1 << LayerMask.NameToLayer("Player"));

        if (targetsInViewRadius.Length <= 0)
        {
            QuestManager.Instance.PopDown();
            return;
        }

        Transform target = default;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform targetTemp = targetsInViewRadius[i].transform;

            target = targetTemp;

            //Vector3 dirToTemp = (targetTemp.position - transform.position).normalized;

            //if (Vector3.Angle(transform.forward, dirToTemp) < viewAngle / 2)
            //{

            //    if (target == default)
            //    {
            //        target = targetTemp;
            //    }
            //    else
            //    {
            //        float dstToTemp = Vector3.Distance(transform.position, targetTemp.position);
            //        float dstToTarget = Vector3.Distance(transform.position, target.position);

            //        if (dstToTarget > dstToTemp)
            //        {
            //            target = targetTemp;
            //        }
            //    }
            //}
        }

        Vector3 dirToTarget = (target.position - transform.position).normalized;

        QuestManager.Instance.PopUp(dirToTarget);
        QuestManager.Instance.ActivateMain(dialogs[0]);
    }
}

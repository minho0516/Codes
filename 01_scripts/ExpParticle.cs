using System.Collections;
using UnityEngine;

public class ExpParticle : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeletePar());
    }
    //��ƼŬ�� ���ִ� ��ũ��Ʈ

    private IEnumerator DeletePar()
    {
        yield return new WaitForSeconds(2f);
        ParticleManager.Instance.Push(this);
    }
}

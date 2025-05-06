using System.Collections;
using UnityEngine;

public class ExpParticle : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeletePar());
    }
    //파티클에 들어가있는 스크립트

    private IEnumerator DeletePar()
    {
        yield return new WaitForSeconds(2f);
        ParticleManager.Instance.Push(this);
    }
}

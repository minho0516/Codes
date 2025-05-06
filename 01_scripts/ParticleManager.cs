using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [SerializeField] ExpParticle _par;
    [SerializeField] Transform _playerTrm;


    private Stack<ExpParticle> _parPool;

    private void Awake()
    {
        Instance = this;
        _parPool = new Stack<ExpParticle>(10);
    }

    public void CreateParticle()
    {
        if (_playerTrm == null) return;

        ExpParticle newPar = null;

        if(_parPool.Count > 0)
        {
            newPar = _parPool.Pop();
            newPar.gameObject.SetActive(true);
            newPar.transform.position = _playerTrm.position;
        }
        else
        {
            newPar = Instantiate(_par, _playerTrm.position, Quaternion.identity);
        }
    }

    public void Push(ExpParticle par)
    {
        par.gameObject.SetActive(false);
        _parPool.Push(par);
    }
}

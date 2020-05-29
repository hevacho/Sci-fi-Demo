using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    [SerializeField]
    private GameObject _otherObjectIntantiate;

    public void DestroyComponent()
    {
        if (_otherObjectIntantiate!=null)
        {
            Instantiate(_otherObjectIntantiate, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}

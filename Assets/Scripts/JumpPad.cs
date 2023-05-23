using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpPower, boostStrength;

    private void OnTriggerEnter(Collider _col)
    {

        if(_col.TryGetComponent<Rigidbody>(out Rigidbody _rb))
            _rb.velocity = new Vector3(_rb.velocity.x, jumpPower, _rb.velocity.z);
        
        if(_col.tag == "Player")
            _col.GetComponent<CoobController>().ApplyBoost(boostStrength);

    }

}

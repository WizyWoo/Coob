using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField]
    private float rotSpeed, bounceSpeed, boostStrength;
    [SerializeField]
    private Transform gfx;
    [SerializeField]
    GameObject coinVFX;
    private Vector3 startPos;

    private void Start()
    {

        startPos = gfx.position;
        boostStrength = Random.Range(5f, 15f + (transform.position.z / 100));

    }

    private void OnTriggerEnter(Collider _col)
    {

        if(_col.tag != "Player")
            return;

        UIManager.Instance.CollectedCoin();
        _col.GetComponent<CoobController>().ApplyBoost(boostStrength);
        Instantiate(coinVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void FixedUpdate()
    {

        gfx.rotation = Quaternion.Euler(Time.time * rotSpeed, Time.time * rotSpeed, 0);
        gfx.position = startPos + new Vector3(0, (Mathf.Sin((Time.time + gfx.position.z) * bounceSpeed) / 4f) + 0.25f, 0);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoobController : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField]
    private float speed, force, controlPower, maxControlSpeed, xDrag, boostFalloff, crashVFXThreshold, destroyTime;
    [SerializeField]
    private GameObject crashVFX, crashPlayerVFX;
    private float boostSpeed, boostTimer;
    private bool lost;

    private void Start()
    {

        Application.targetFrameRate = 60;
        rb = gameObject.GetComponent<Rigidbody>();
        UIManager.Instance.PlayerRB = rb;

    }

    private void Update()
    {

        if(transform.position.y < -1)
        {
            rb.drag = 10;
            if(lost)
                return;

            lost = true;
            UIManager.Instance.Lost();
        }

        int _xDir = 0;

        if(Input.touchCount > 0)
            _xDir = Input.GetTouch(0).position.x > Screen.currentResolution.width / 2 ? 1 : -1;
        //_xDir = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (_xDir * controlPower * Time.deltaTime), -maxControlSpeed, maxControlSpeed) * xDrag, rb.velocity.y, rb.velocity.z);
        if(rb.velocity.z < speed)
            rb.AddForce(Vector3.forward * (speed * force * Time.deltaTime), ForceMode.Force);

        if(boostSpeed > 0 && boostTimer <= 0)
        {
            boostSpeed -= Time.deltaTime * boostFalloff;
        }
        else
            boostTimer -= Time.deltaTime;

    }

    private void OnCollisionEnter(Collision _col)
    {

        if(rb.velocity.magnitude < crashVFXThreshold)
            return;

        if(_col.gameObject.TryGetComponent<Rigidbody>(out Rigidbody _rb))
            Destroy(Instantiate(crashVFX, _col.GetContact(0).point, Quaternion.LookRotation(_col.GetContact(0).normal)), destroyTime);
        else
            Destroy(Instantiate(crashPlayerVFX, _col.GetContact(0).point, Quaternion.LookRotation(_col.GetContact(0).normal)), destroyTime);


    }

    public void ApplyBoost(float _boostStrength)
    {

        rb.AddForce(Vector3.forward * _boostStrength, ForceMode.Impulse);

    }

}

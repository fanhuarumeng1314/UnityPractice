using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Lbs : MonoBehaviour {

    public float rotaSpeed = 50.0f;

    void Start()
    {

    }

    void Update()
    {
        Rota();

    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayCharacter>();
        if (player)
        {
            PlayGameMode.Instance.AddProp("1017");
            Destroy(gameObject);
        }
    }

    public void Rota()
    {
        transform.Rotate(Vector3.up, rotaSpeed * Time.deltaTime);
    }
}

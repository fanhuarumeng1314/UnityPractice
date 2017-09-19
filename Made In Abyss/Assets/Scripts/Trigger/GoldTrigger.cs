using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTrigger : MonoBehaviour {

    public float rotaSpeed = 50.0f;

	void Start () {
		
	}
	
	void Update ()
    {
        Rota();

    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayCharacter>();
        if (player)
        {
            player.AddMoney(10);
            PlayGameMode.Instance.AddMoney(10,transform);
            gameObject.SetActive(false);
        }
    }

    public void Rota()
    {
        transform.Rotate(Vector3.up, rotaSpeed * Time.deltaTime);
    }
}

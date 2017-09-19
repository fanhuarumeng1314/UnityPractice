using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var plar = other.GetComponent<PlayCharacter>();

        if (plar)
        {
            PlayGameMode.Instance.AddProp(RandomProp());
            PlayGameMode.Instance.ChangeMaps(transform);
            gameObject.SetActive(false);
        }
    }

    public string RandomProp()
    {
        int number =Random.Range(1, PropLibry.Instance.propTable.Count);
        int tmp_Number = 0;
        foreach (var prop in PropLibry.Instance.propTable)
        {
            tmp_Number++;
            if (tmp_Number>=number)
            {
                return prop.Key;
            }
        }

        return null;
    }
}

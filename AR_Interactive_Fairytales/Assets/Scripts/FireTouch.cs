using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireTouch : MonoBehaviour
{

    [SerializeField] private List<GameObject> FireList = new List<GameObject>();
    //[SerializeField] private GameObject[] Fire = new GameObject[2];
    private string handTag = "Player";
    private int count  = 0;
    //[SerializeField] public Text ScriptTxt;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FireList.Count; i++)
        {
            FireList[i].SetActive(false);           //set all fire unactive
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ScriptTxt.text = "touch" + count;
        ChangeFire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(handTag))
        {
            count += 1;

            Handheld.Vibrate();
        }

    }

    private void ChangeFire()
    {
        if(count == 1)
        {
            FireList[0].SetActive(true);
        }
        else if(count == 3)
        {
            FireList[0].SetActive(false);
            FireList[1].SetActive(true);
        }
        else if (count == 5)
        {
            FireList[1].SetActive(false);
            FireList[2].SetActive(true);
        }
    }
}

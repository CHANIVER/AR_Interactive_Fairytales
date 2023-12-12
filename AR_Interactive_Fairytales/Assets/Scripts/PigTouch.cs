using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class PigTouch : MonoBehaviour
{

    [SerializeField] private Material[] materials = new Material[2];
    [SerializeField] private Transform Target;
    public float Speed = 1f;
    private string handTag = "Player";
    //private bool isGrabbing;
    private float skeletonConfidence = 0.0001f;
    //[SerializeField] public Text ScriptTxt;
    private int count = 1;
    public float time;
    public int num;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        num = ((int)time);

        ManomotionManager.Instance.ShouldCalculateGestures(true);

        

        bool hasConfidence = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.confidence > skeletonConfidence;


        //ScriptTxt.text = "touch" + count + "\n" + "score" + (count - (num / 5));

        transform.position = Vector3.MoveTowards(transform.position, Target.position, Mathf.Log10(count) * Time.deltaTime*Speed);
        //transform.Translate(Vector3.right * Time.deltaTime * Mathf.Log10(count));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(handTag))
        {
            //objcectRenderer.sharedMaterial = materials[1];
            count += 1;

            //transform.position = transform.position + new Vector3(1, 0, 0);
            Handheld.Vibrate();
        }
        else if (other.gameObject.CompareTag("House"))
        {
            //objcectRenderer.sharedMaterial = materials[1];
            Destroy(gameObject);
            Speed = 0; //¸ØÃß±â
            //ScriptTxt.text = "µµÂø";
            //transform.position = transform.position + new Vector3(1, 0, 0);
            Handheld.Vibrate();
        }

    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class ObjectManipulator : MonoBehaviour
{

    [SerializeField] private Material[] materials = new Material[2];
    private Renderer objcectRenderer;
    private string handTag = "Player";
    private bool isGrabbing;
    private float skeletonConfidence = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        objcectRenderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        ManomotionManager.Instance.ShouldCalculateGestures(true);

        var currentGesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger;


        if (currentGesture == ManoGestureTrigger.PICK)
        {
            isGrabbing = true;

        }

        else if (currentGesture == ManoGestureTrigger.DROP)
        {
            isGrabbing = false;
            transform.parent = null;
        }

        bool hasConfidence = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.confidence > skeletonConfidence;

        if (!hasConfidence)
        {
            objcectRenderer.sharedMaterial = materials[0];

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            // ���� ��ġ�� ������
            Vector3 currentPosition = transform.position;

            // y ��ǥ�� 1�� ����
            currentPosition.y = 1;

            // ������ ��ġ�� �ٽ� �Ҵ�
            transform.position = currentPosition;

        }

        if (other.gameObject.CompareTag(handTag))
        {
            objcectRenderer.sharedMaterial = materials[1];
            //Handheld.Vibrate();
        }

        else if (isGrabbing)
        {
            transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(handTag) && isGrabbing)
        {
            objcectRenderer.sharedMaterial = materials[1];
            transform.parent = other.gameObject.transform;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;
        objcectRenderer.sharedMaterial = materials[0];
    }
}
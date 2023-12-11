using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR.ARFoundation;

public class UIManipulator : MonoBehaviour
{
    bool isTouching = false; // �ߺ� ��ġ ���� isTouching ����
    bool isUIOff = false;
    [SerializeField] private GameObject scenario;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true); //UI Ȱ��ȭ
        scenario.SetActive(false);  //������Ʈ ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        TurnUIOff();
    }
    private void TurnUIOff()
    {
        if (!isTouching && Input.touchCount > 0)   // Touch���̸� �ߺ���ġ �ȵǵ��� ����
        {
            gameObject.SetActive(false);
            isUIOff = true;
        }
        if(isUIOff)
        {
            scenario.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARImageTracking : MonoBehaviour
{
    public Text TrackingPage;
    //public Text CreatedImage;
    public float _timer;    // �� �� Limited �����̸� �̹����� ��Ȱ��ȭ �� ���� ���� (����� �ʿ�x)
    public ARTrackedImageManager trackedImageManager;
    public List<GameObject> _objectList = new List<GameObject>();
    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();
    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();
    private List<float> _trackedTimer = new List<float>();
    void Awake()
    {
        TrackingPage.text = "None";
        //CreatedImage.text = "None";

        foreach (GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj); // �̸��� �̿��Ͽ� access�� ����
        }

    }

    void Update()
    {
        if (_trackedImg.Count >= 0)   // �̹����� Ʈ��ŷ ���̸�
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();
            for (var i = 0; i < _trackedImg.Count; i++)
            {
                if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                // TrackingState�� Limited�� ��
                {
                    //if (_trackedTimer[i] > 0.03){}
                    string name = _trackedImg[i].referenceImage.name;
                    GameObject tObj = _prefabDic[name];
                    tObj.SetActive(false);
                    //CreatedImage.text = "Removed " + name;
                    //tNumList.Add(_trackedImg[i]);   // Ʈ��ŷ�� ���� �� �̹��� ��Ͽ� �߰�

                    /*else
                    {
                        _trackedTimer[i] += Time.deltaTime;     // Timer ��ø
                        text1.text = (i.ToString() + ", " + _trackedTimer[i].ToString());
                    }
                    */

                }
            }

            if (tNumList.Count > 0)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    int num = _trackedImg.IndexOf(tNumList[i]);
                    _trackedImg.Remove(_trackedImg[num]);       // Ʈ��ŷ�ϰ��ִ� �̹������� ����
                    TrackingPage.text = "Removed" + name + "\n" + _trackedImg.Count;
                    // _trackedTimer.Remove(_trackedTimer[num]);   // Ÿ�̸� Ʈ��ŷ ����
                }
            }
        }
        
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        //trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)    // add, update, remove
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (!_trackedImg.Contains(trackedImage))    // �̹����� ����Ʈ�� ���� ���
            {
                string name = trackedImage.referenceImage.name;
                _trackedImg.Add(trackedImage);
                TrackingPage.text = "Tracking" + name;
                _trackedTimer.Add(0);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (!_trackedImg.Contains(trackedImage))    // �̹����� ����Ʈ�� ���� ���
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else                                        // �̹����� ����Ʈ�� �̹� ���� ���
            {
                int num = _trackedImg.IndexOf(trackedImage);
                _trackedTimer[num] = 0;
            }
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name; // ���ڷ� �Ѱܹ��� trakcedImage�� �̸��� ������
        GameObject tObj = _prefabDic[name];             // Dic���� ������Ʈ�� ������
        // �̹����� ��ġ�� ������Ʈ ��ȯ
        tObj.transform.position = trackedImage.transform.position;
        tObj.transform.rotation = trackedImage.transform.rotation;
        tObj.SetActive(true);
        //CreatedImage.text = "Create " + name;
    }
}

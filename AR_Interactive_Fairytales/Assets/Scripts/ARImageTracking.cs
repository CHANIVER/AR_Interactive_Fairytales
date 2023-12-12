using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Threading;

public class ARImageTracking : MonoBehaviour
{
    public Text TrackingPage;
    public float _timer;
    public ARTrackedImageManager trackedImageManager;
    public List<GameObject> _objectList = new List<GameObject>();
    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();
    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();
    private List<float> _trackedTimer = new List<float>();

    private int count = 0;  //???????? ??????????

    void Awake()
    {
        foreach (GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj);
        }
    }

    void Update()
    {
        if (_trackedImg.Count == 0)      //???????? ?????????? 1??????????
        {
            foreach (GameObject obj in _objectList)
            {
                obj.SetActive(false);
            }
        }
        




        //TrackingPage.text = "000";
        if (_trackedImg.Count > 0)
        {
            //TrackingPage.text = "111";
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();
            for (var i = 0; i < _trackedImg.Count; i++)
            {
                if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                    //TrackingPage.text = "222"+_timer+";;"+_trackedTimer[i];
                    if (_trackedTimer[i] > _timer)
                    {
                        string name = _trackedImg[i].referenceImage.name;
                        GameObject tObj = _prefabDic[name];
                        tObj.SetActive(false);
                        tNumList.Add(_trackedImg[i]);
                    }
                    else
                    {
                        _trackedTimer[i] += Time.deltaTime;
                    }
                }
            }

            if (tNumList.Count > 0)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    int num = _trackedImg.IndexOf(tNumList[i]);
                    _trackedImg.Remove(_trackedImg[num]);
                    _trackedTimer.Remove(_trackedTimer[num]);

                    TrackingPage.text = "???? ?????????? ???????? ???????? ????????.";
                }
            }
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
        TrackingPage.text = "O";
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
        TrackingPage.text = "X";
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else
            {
                int num = _trackedImg.IndexOf(trackedImage);
                _trackedTimer[num] = 0;
            }
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        //hyuntatk
        /*foreach (GameObject obj in _objectList)
        {
            string objname = obj.name;
            _prefabDic[objname].SetActive(false);
        }*/
        //hyuntak


        GameObject tObj = _prefabDic[name];

        //tObj.transform.position = trackedImage.transform.position*50;
        tObj.transform.position = new Vector3(trackedImage.transform.position.x, trackedImage.transform.position.y - 10.0f, trackedImage.transform.position.z + 7.5f);
        
        //tObj.transform.rotation = trackedImage.transform.rotation;
        //TrackingPage.text = "Tracking" + name;
        tObj.SetActive(true);
        count += 1;
        //TrackingPage.text = "Tracking" + tObj.transform.position;
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace ARWT.Marker{
    public class GenericController : MonoBehaviour
    {

        //public string markerToListen = "hiro";
        public string markerToListen1;
        public string markerToListen2;

        public GameObject child1;
        public GameObject child2;
        public GameObject child3;

        public float updateSpeed = 10;
        public float positionThreshold = 0;

        public GameObject uiHelper, gameUI;

        bool firstTime = true;

        bool _hasFoundMarker;
        bool _hasWaited = false;
        private float _scanTimeWaitLimit = 1.5f;
        private float _scanTimer;




        void Start()
        {
            //DetectionManager.onMarkerVisible += onMarkerVisibleOnce;
            //DetectionManager.onMarkerLost += onMarkerLostOnce;

            DetectionManager.onMarkerVisible += onMarkerVisibleNormal;
            DetectionManager.onMarkerLost += onMarkerLostNormal;
        }




        //void onMarkerVisible(MarkerInfo m)
        //{
        //    if (m.name == markerToListen1) {

        //        //child1?.SetActive(true);
        //        child3?.SetActive(true);
        //        uiHelper?.SetActive(false);
        //        //gameUI?.SetActive(true);

        //        if (!firstTime)
        //        {
        //            if (Vector3.Distance(m.position, transform.position) > positionThreshold)
        //            {
        //                transform.position = Vector3.Lerp(transform.position, m.position, Time.deltaTime * updateSpeed);
        //            }
        //        }
        //        else
        //        {
        //            transform.position = m.position;
        //            firstTime = false;
        //        }

        //        transform.rotation = m.rotation;

        //        Vector3 absScale = new Vector3(
        //            Mathf.Abs(m.scale.x),
        //            Mathf.Abs(m.scale.y),
        //            Mathf.Abs(m.scale.z)
        //        );

        //        transform.localScale = absScale / 2;
        //    }
        //    else if (m.name == markerToListen2)
        //    {

        //        child2?.SetActive(true);
        //        uiHelper?.SetActive(false);
        //        //gameUI?.SetActive(true);

        //        if (!firstTime)
        //        {
        //            if (Vector3.Distance(m.position, transform.position) > positionThreshold)
        //            {
        //                transform.position = Vector3.Lerp(transform.position, m.position, Time.deltaTime * updateSpeed);
        //            }
        //        }
        //        else
        //        {
        //            transform.position = m.position;
        //            firstTime = false;
        //        }

        //        transform.rotation = m.rotation;

        //        Vector3 absScale = new Vector3(
        //            Mathf.Abs(m.scale.x),
        //            Mathf.Abs(m.scale.y),
        //            Mathf.Abs(m.scale.z)
        //        );

        //        transform.localScale = absScale / 2;
        //    }
        //}

        void onMarkerVisibleOnce(MarkerInfo m)
        {
            if (m.name == markerToListen1)
            {
                // activate a timer (1.5 seconds) -> after this timer, spawn in bim
                // on marker exit, if bim has not been found -> reset timer
                if (_hasFoundMarker == false)
                {
                    ActivateScanTimer();
                }


                if (_hasFoundMarker == false && _hasWaited == true)
                {
                    child3?.SetActive(true);
                    uiHelper?.SetActive(false);

                    // setting the position
                    if (firstTime == true)
                    {
                        transform.position = m.position;
                        //transform.position = Camera.main.transform.position;
                        firstTime = false;
                    }

                    // bool set
                    _hasFoundMarker = true;
                }
            }
        }

        void onMarkerVisibleNormal(MarkerInfo m)
        {
            if (m.name == markerToListen1)
            {
                child3?.SetActive(true);
                uiHelper?.SetActive(false);

                // setting the position
                if (!firstTime)
                {
                    if (Vector3.Distance(m.position, transform.position) > positionThreshold)
                    {
                        transform.position = Vector3.Lerp(transform.position, m.position, Time.deltaTime * updateSpeed);
                    }
                }
                else
                {
                    transform.position = m.position;
                    firstTime = false;
                }

                // setting the rotation
                transform.rotation = m.rotation;

                // setting the scale
                //Vector3 absScale = new Vector3(
                //    Mathf.Abs(m.scale.x),
                //    Mathf.Abs(m.scale.y),
                //    Mathf.Abs(m.scale.z)
                //);
                //transform.localScale = absScale / 2;
            }
        }

        //void onMarkerLost(MarkerInfo m)
        //{
        //    if(m.name == markerToListen1){
        //        child3?.SetActive(false);
        //        //child1?.SetActive(false);
        //        uiHelper?.SetActive(true);
        //        //gameUI?.SetActive(false);
        //        firstTime = true;
        //    }
        //    else if (m.name == markerToListen2)
        //    {
        //        child2?.SetActive(false);
        //        uiHelper?.SetActive(true);
        //        //gameUI?.SetActive(false);
        //        firstTime = true;
        //    }
        //}

        void onMarkerLostOnce(MarkerInfo m)
        {
            if (m.name == markerToListen1)
            {
                if (_hasWaited == false)
                {
                    _scanTimer = 0;
                }
            }
        }
        void onMarkerLostNormal(MarkerInfo m)
        {
            if (m.name == markerToListen1)
            {
                child3?.SetActive(false);
                uiHelper?.SetActive(true);

                firstTime = true;
            }
        }


        private void ActivateScanTimer()
        {
            _scanTimer += Time.deltaTime;

            if (_scanTimer >= _scanTimeWaitLimit)
            {
                _hasWaited = true;
                _scanTimer = 0;
            }
        }
    }
}

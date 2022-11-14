using UnityEngine;
using UnityEngine.UI;

namespace ARWT.Marker{
    public class GenericController : MonoBehaviour{

        //public string markerToListen = "hiro";
        public string markerToListen1 = "Bim_1";
        public string markerToListen2 = "Mouse";

        public GameObject child1;
        public GameObject child2;

        public float updateSpeed = 10;
        public float positionThreshold = 0;

        public GameObject uiHelper, gameUI;

        bool firstTime = true;

        void Start() {
            DetectionManager.onMarkerVisible += onMarkerVisible;
            DetectionManager.onMarkerLost += onMarkerLost;
        }

        void onMarkerVisible(MarkerInfo m)
        {
            if (m.name == markerToListen1) {

                child1?.SetActive(true);
                uiHelper?.SetActive(false);
                gameUI?.SetActive(true);

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

                transform.rotation = m.rotation;

                Vector3 absScale = new Vector3(
                    Mathf.Abs(m.scale.x),
                    Mathf.Abs(m.scale.y),
                    Mathf.Abs(m.scale.z)
                );

                transform.localScale = absScale / 2;
            }
            else if (m.name == markerToListen2)
            {

                child2?.SetActive(true);
                uiHelper?.SetActive(false);
                gameUI?.SetActive(true);

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

                transform.rotation = m.rotation;

                Vector3 absScale = new Vector3(
                    Mathf.Abs(m.scale.x),
                    Mathf.Abs(m.scale.y),
                    Mathf.Abs(m.scale.z)
                );

                transform.localScale = absScale / 2;
            }
        }

        void onMarkerLost(MarkerInfo m)
        {
            if(m.name == markerToListen1){
                child1?.SetActive(false);
                uiHelper?.SetActive(true);
                gameUI?.SetActive(false);
                firstTime = true;
            }
            else if (m.name == markerToListen2)
            {
                child2?.SetActive(false);
                uiHelper?.SetActive(true);
                gameUI?.SetActive(false);
                firstTime = true;
            }
        }
    }
}

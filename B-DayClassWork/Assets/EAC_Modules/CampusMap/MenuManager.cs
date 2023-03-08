using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DigitalSalmon.C360
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject panel;
        public MouseLook look;
        public Sprite blackPin;
        public Sprite defaultPin;
        public Complete360Tour tour;

        public void Start()
        {
            look = GameObject.Find("Camera").GetComponent<MouseLook>();
        }

        public void Activate()
        {
            panel.SetActive(!panel.activeSelf);
            if (panel.active) { CheckCurrentPosition();}
            look.enabled = !look.enabled;
        }

        //Todo. Loop through children in panel when active, get all scripts within it. Check if current node is the current one.
        //Set that icon to the black icon. And set all others to Blue.

        void CheckCurrentPosition()
        {
            //Get all the pins.
            MapNodeChange[] pinData = panel.GetComponentsInChildren<MapNodeChange>();
            foreach(MapNodeChange x in pinData)
            {
                if (tour.GetActiveNode().Name == x.nodeToSwitch.name)
                {
                    x.gameObject.GetComponent<Image>().sprite = blackPin;
                    Debug.Log("scrolled!");
                }
                else
                {
                    x.gameObject.GetComponent<Image>().sprite = defaultPin;
                }
            }
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DigitalSalmon.C360
{
    public class NodeSwapTextBox : MonoBehaviour
    {
        //public Complete360Tour c360;
        private Text text;


        void Start()
        {
            text = transform.GetComponent<Text>();
        }

        public void MouseOver(string name)
        {
            text.text = name;
        }

        public void MouseOff()
        {
            text.text = "";
        }
    }
}
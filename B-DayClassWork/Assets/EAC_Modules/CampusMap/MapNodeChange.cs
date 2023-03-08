using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace DigitalSalmon.C360
{
    public class MapNodeChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //Name of node to switch to
		//Update: Changed to Node type for consistency.
        //public string nodeToSwitch;
        public Node nodeToSwitch;

        //Name that is displayed on map
        public string prettyName;

        //Tour that the node is in
        public Tour tourToSwitch;

        //Text box that displays name of nodes
        private NodeSwapTextBox locationName;

        //C360
        private Complete360Tour c360;

        //Start hooks for buttons, get rid of all that other mess.

        public void Start()
        {
            c360 = GameObject.Find("Complete360Tour").GetComponent<Complete360Tour>();
            locationName = GameObject.Find("Location").GetComponent<NodeSwapTextBox>();
        }

        public void OnButtonClick()
        {
            //Swap(nodeToSwitch, tourToSwitch);
            Swap(nodeToSwitch); //Changed to just one node rather than a tour/node pair.
            GameObject.Find("Map").SetActive(false);
            GameObject.Find("Camera").GetComponent<MouseLook>().enabled = true;
        }

        //Typical swap nodes, but also checks other tours if node isn't in this tour
		//Update: Requires a Node type to switch to, not using separate Tours at present.
        //public void Swap(Node node, Tour tour)
        public void Swap(Node node)
        {
            //if (tour.name == Tour.Current.name)
            //{
                Complete360Tour.GoToMedia(node);
           // }
           //else
           // {
            //    c360.SwapTour(tour, node);
           // }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            locationName.MouseOver(prettyName);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            locationName.MouseOff();
        }
    }


    [System.Serializable]
    public class BasicNode
    {
        public Node node;
        public Tour tour;
    }

}

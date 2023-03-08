using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DigitalSalmon.C360
{
    // -----------------------------------------------------------------------------------------------------//
    // - Only have this script in a scene when manipulating the JSON file. Be sure it is NOT active         //
    // - upon a build.                                                                                      //
    // - To use:                                                                                            //
    // -    1. Put this script onto an empty gameobject, give it a reference to the tour                    //
    // -    2. In play mode, press G to activate editing                                                    //
    // -    3. Move the camera to the direction you desire it to look upon entering a scene                 //
    // -    4. Press C to add the current rotation of the camera to the dictionary.                         //
    // -    5. If the current node combination is already in the dictionary, you will be notified. You      //
    // -       may use shift + C to overwrite a previously made rotation.                                   //
    // -    6. Once you have all of the rotations done, press 7 to write the compiled rotations to the JSON //
    // -       file.                                                                                        //
    // -----------------------------------------------------------------------------------------------------//

    public class CameraRotationSaver : MonoBehaviour
    {
        public Complete360Tour tour = null;
        public MapNodeChange nodeChange = null;
        
        private Dictionary<(string, string), Vector3> cameraRotations = new Dictionary<(string, string), Vector3>();
        private Dictionary<string, BasicNode> nodeList = new Dictionary<string, BasicNode>();

        private bool canEdit = false;
        private bool dictionaryInitialized = false;

        // Update is called once per frame
        void Update()
        {
            canEditDictionary();

            //If dictionary is not filled from JSON file, fill it
            if(!dictionaryInitialized)
            {
                ReadJSON();
            }

            if (canEdit)
            {


                //If user presses shift + c, take the current camera rotation and put it to JSON regardless of contents
                //If user presses only c, check for duplicate entries
                if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
                {
                    CameraTransform transform = ReadCamera();

                    if (cameraRotations.ContainsKey((transform.previousNode, transform.NextNode)))
                    {
                        cameraRotations[(transform.previousNode, transform.NextNode)] = transform.angles;
                        Debug.Log("Previous Entry Overwritten");
                    }
                    else
                    {
                        WriteDictionary(transform);
                        Debug.Log("Entry Written");
                    }

                }
                else if(Input.GetKeyDown(KeyCode.C))
                {
                    CameraTransform transform = ReadCamera();
                    
                    if (cameraRotations.ContainsKey((transform.previousNode, transform.NextNode)))
                    {
                        Debug.Log("You already have a rotation set for this pair of nodes. If you would like to overwrite this rotation, press" +
                            "shift + c");
                    }
                    else
                    {
                        WriteDictionary(transform);
                        Debug.Log("Entry Written");
                    }
                }

                if(Input.GetKeyDown(KeyCode.L))
                {
                    BasicNode node = ReadNode();
                    if (nodeList.ContainsKey(node.node.Name))
                    {
                        Debug.Log("You already have this node.");
                    }
                    else
                    {
                        WriteNodeDictionary(node, nodeList);
                        Debug.Log("Entry Written");
                        Debug.Log(node.node);
                        //Debug.Log(node.tourName);
                    }
                }

                if(Input.GetKeyDown(KeyCode.Alpha7))
                {
                    WriteJSON();
                    //WriteNodeJSON();
                    Debug.Log("Written to JSON");
                }
            }
        }

        //For debugging safety
        public void canEditDictionary()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("SET TO WRITE MODE");
                canEdit = true;
            }
        }

        //Read from a JSON file (whether it exists or not) and put the contents in the temp dictionary
        public void ReadJSON()
        {
            CameraTransform cameraRot;
            using (StreamReader sr = new StreamReader(Application.dataPath+"/Resources/CameraDirectionData.json"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    cameraRot = JsonUtility.FromJson<CameraTransform>(line);
                    WriteDictionary(cameraRot);
                }
            }
            dictionaryInitialized = true;
        }

        //Write current rotation to the temp dictionary
        public void WriteDictionary(CameraTransform transform)
        {
            (string, string) nodes;
            nodes.Item1 = transform.previousNode;
            nodes.Item2 = transform.NextNode;
            cameraRotations.Add(nodes, transform.angles);
        }

        //Read the rotation of the camera, as well as the current and previous nodes
        public CameraTransform ReadCamera()
        {
            CameraTransform transform = new CameraTransform();
            transform.angles = Camera.main.transform.eulerAngles;
            Debug.Log(transform.angles);
            transform.previousNode = tour.PreviousNode.Name;
            transform.NextNode = tour.NextNode.Name;
            return transform;
        }

        //Take everything in the temp dictionary and write it to JSON
        public void WriteJSON()
        {
            Debug.Log(Application.dataPath);
            using (StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.dataPath, "Resources/CameraDirectionData.json"), false))
            {
                foreach (KeyValuePair<(string, string), Vector3> entry in cameraRotations)
                {
                    CameraTransform cameraRot = new CameraTransform
                    {
                        angles = entry.Value,
                        previousNode = entry.Key.Item1,
                        NextNode = entry.Key.Item2
                    };
                    string toWrite = JsonUtility.ToJson(cameraRot);
                    {
                        file.WriteLine(toWrite);
                    }
                }
            }
        }

        public void WriteNodeDictionary(BasicNode node, Dictionary<string, BasicNode> list)
        {
            list.Add(node.node.Name, node);
        }

        public BasicNode ReadNode()
        {
            BasicNode node = new BasicNode();
            node.node = tour.GetActiveNode();
            node.tour = Tour.Current;
            return node;
        }
/*
        public void WriteNodeJSON()
        {
            using (StreamWriter file = new StreamWriter("../NodeList.json", false))
            {
                foreach (KeyValuePair<string, BasicNode> entry in nodeList)
                {
                    string toWrite = JsonUtility.ToJson(entry.Value);
                    {
                        file.WriteLine(toWrite);
                    }
                }
            }
        }
        */
    }

    [System.Serializable]
    public class CameraTransform
    {
        public Vector3 angles;
        public string previousNode;
        public string NextNode;
    }


}
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DigitalSalmon.C360 {
	[AddComponentMenu("Complete 360 Tour/Core/Entry Yaw Reactor")]
	public class EntryYawReactor : MediaReactor {
		//-----------------------------------------------------------------------------------------
		// Type Definitions:
		//-----------------------------------------------------------------------------------------

		Dictionary<(string, string), Vector3> cameraRotations = new Dictionary<(string, string), Vector3>();
		Complete360Tour tour;
		(string, string) nodes;


		public enum EntryYawModes {
			Absolute,
			Dynamic
		}

		//-----------------------------------------------------------------------------------------
		// Inspector Variables:
		//-----------------------------------------------------------------------------------------

		[SerializeField]
		[Tooltip("Absolute | The world is aligned to the entry yaw defined by the node editor.\n Dynamic | The camera will always be looking at the entry yaw line when the node is entered")]
		protected EntryYawModes entryYawMode;

		//-----------------------------------------------------------------------------------------
		// Private Fields:
		//-----------------------------------------------------------------------------------------

		private CameraBase cameraBase;

		//-----------------------------------------------------------------------------------------
		// Unity Lifecycle:
		//-----------------------------------------------------------------------------------------

		protected override void Awake() {
			base.Awake();
			cameraBase = FindObjectOfType<CameraBase>();

			tour = GameObject.Find("Complete360Tour").GetComponent<Complete360Tour>();
			CameraTransform cameraRot;
            //Debug.Log(Application.streamingAssetsPath);
			//(string, string) nodes;
            var json = UnityEngine.Resources.Load<TextAsset>("CameraDirectionData");
            MemoryStream stream = new MemoryStream(json.bytes);

			using (StreamReader sr = new StreamReader(stream))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					cameraRot = JsonUtility.FromJson<CameraTransform>(line);
					//Debug.Log(cameraRot.angles);
					nodes.Item1 = cameraRot.previousNode;
					nodes.Item2 = cameraRot.NextNode;
					cameraRotations.Add(nodes, cameraRot.angles);
				}
			}
		}

		protected override void C360_MediaSwitch(TransitionState state, Node node) {
			if (!(node is MediaNode mediaNode)) return;
			if (state == TransitionState.Switch) UpdateEntryYaw(mediaNode.EntryYaw);
		}

		//-----------------------------------------------------------------------------------------
		// Private Methods:
		//-----------------------------------------------------------------------------------------

		private void UpdateEntryYaw(float entryYaw) {
			if (cameraBase == null) return;
			switch (entryYawMode) {
				case EntryYawModes.Absolute:
					cameraBase.SetYaw(entryYaw * 360);
					break;
				case EntryYawModes.Dynamic:


/*
					float cameraYaw = cameraBase.GetCameraYaw();
					cameraBase.SetYaw(entryYaw * 360 - cameraYaw);
					break;
*/
					float cameraYaw = cameraBase.GetCameraYaw();
					
					//(string, string) nodes;
					if (tour.PreviousNode != null)
					{
						nodes.Item1 = tour.PreviousNode.Name;
						nodes.Item2 = tour.NextNode.Name;
						Debug.Log(nodes.Item1);
						Debug.Log(nodes.Item2);
						if (cameraRotations.ContainsKey(nodes))
						{
							//cameraBase.SetYaw(cameraRotations[nodes].x - cameraBase.GetCameraYee(), entryYaw * 360 - cameraYaw);
							Debug.Log("dictionary print");
							Debug.Log(cameraRotations[nodes]);
							UnityEngine.Camera.main.gameObject.GetComponent<MouseLook>().SetDirection(Quaternion.Euler(cameraRotations[nodes]));
							Debug.Log("hit");
						}
						else
						{
							Debug.Log("Miss");
							cameraBase.SetYaw(entryYaw * 360 - cameraYaw);
						}
					}
					break;


			}
		}
	}
}
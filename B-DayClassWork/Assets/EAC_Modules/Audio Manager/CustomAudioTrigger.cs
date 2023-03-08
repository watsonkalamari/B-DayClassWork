using DigitalSalmon.Extensions;
using UnityEngine;
using TMPro;

//Resize canvas acording with numbers of characters in Title.

namespace DigitalSalmon.C360.AnimatedComponents
{
    [AddComponentMenu("Complete 360 Tour/Animated Components/Custom Trigger Audio")]
    public class CustomAudioTrigger : BaseBehaviour
    {
        //-----------------------------------------------------------------------------------------
        // Inspector Variables:
        //-----------------------------------------------------------------------------------------
        [SerializeField]
        protected AudioClip audioClip;

        public AudioManager audioSource;

        [SerializeField]
        protected AudioManager.AudioSourceType AudioType = AudioManager.AudioSourceType.NARRATION;

        protected virtual void Awake()
        {
            //	audioSource = GetComponent<AudioSource>();
            //Wesley Modifications to work with AudioManger
            audioSource = FindObjectOfType<AudioManager>();
        }

        private void Start()
        {
            if (AudioType == AudioManager.AudioSourceType.MUSIC || AudioType == AudioManager.AudioSourceType.AMBIANCE || AudioType == AudioManager.AudioSourceType.NARRATION)
            {
                audioSource.PlayAudio(AudioType, audioClip);
            }
        }
		
		public void TriggerAudio(){
            audioSource.PlayAudio(AudioType, audioClip);
		}

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.parent.parent.name == gameObject.name)
                    {
                            audioSource.PlayAudio(AudioType, audioClip);
                    }
                }
            }
        }
    }
}
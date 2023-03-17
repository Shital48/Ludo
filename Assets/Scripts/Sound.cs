using UnityEngine; 

namespace com.ludoGame
{
    public class Sound : MonoBehaviour
    { 
        public static void PlaySound(AudioClip clip)
        {
            PlaySound(clip, MatchManager.Instance.audioSource);
        }

        public static void PlaySound(AudioClip clip, AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.time = 0;
            audioSource.loop = false;
            audioSource.Play();
        }
         
    }
     
}
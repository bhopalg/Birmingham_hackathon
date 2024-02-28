using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Timers;
using System;
public class VignettePulse : MonoBehaviour
{
   PostProcessVolume m_Volume;
   Vignette m_Vignette;

   private float birth = 0.0f;
   private float age = 0.0f;
   
   void Start()
  {
      // Create an instance of a vignette
        birth = Time.realtimeSinceStartup;
       m_Vignette = ScriptableObject.CreateInstance<Vignette>();
       m_Vignette.enabled.Override(true);
       m_Vignette.intensity.Override(1f);
      // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
       m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
  }

   void Update()
  {
    this.age=Time.realtimeSinceStartup-birth;
       // Change vignette intensity using a sinus curve
        m_Vignette.intensity.value = Mathf.Sin(age);

        if (age>10){
            Destroy(this.gameObject);
        }
  }
   void OnDestroy()
  {
       RuntimeUtilities.DestroyVolume(m_Volume, true, true);
  }
}
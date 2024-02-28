using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Timers;
using System;
public class VignettePulse : MonoBehaviour
{
   PostProcessVolume m_Volume;
   Vignette m_Vignette;
   LensDistortion m_LensDistortion;

   private float birth = 0.0f;
   private float age = 0.0f;
   
   void Start()
  {
      // Create an instance of a vignette
        birth = Time.realtimeSinceStartup;
       m_Vignette = ScriptableObject.CreateInstance<Vignette>();
       m_LensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
       m_LensDistortion.enabled.Override(true);
       m_LensDistortion.intensity.Override(0f);
       m_Vignette.enabled.Override(true);
       m_Vignette.intensity.Override(1f);
      // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
       m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette, m_LensDistortion);
  }

   void Update()
  {
        this.age=Time.realtimeSinceStartup-birth;
       // Change vignette intensity using a sinus curve
        m_Vignette.intensity.value = Math.Abs(Mathf.Sin(age));
        m_LensDistortion.intensity.value = Mathf.Sin(age) * 50;

        if (age>4*Math.PI){
            Destroy(this.gameObject);
        }
  }
   void OnDestroy()
  {
       RuntimeUtilities.DestroyVolume(m_Volume, true, true);
  }
}
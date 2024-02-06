using System.Collections;
using UnityEngine;

namespace CarnivalShooter.Gameplay {

  public class LightBulb : MonoBehaviour {
    [SerializeField] GameObject lightbulbChildMeshGo;
    private Material m_EmissiveMaterial;
    private bool m_LightIsEnabled;
    private bool m_IsLightToggling;
    private WaitForSeconds m_WaitForSeconds;

    private void Awake() {
      m_EmissiveMaterial = lightbulbChildMeshGo.GetComponent<MeshRenderer>().material;
      m_WaitForSeconds = new WaitForSeconds(Random.Range(0.5f, 1f));
      float disableValue = Random.Range(0f, 1f);
      if (disableValue < 0.5f) {
        EnableEmission();
      } else {
        DisableEmission();
      }
      m_LightIsEnabled = true;
      m_IsLightToggling = false;
    }

    private void OnDisable() {
      m_IsLightToggling = false;
    }

    void Start() {
      StartCoroutine(ToggleLight());
    }

    private void DisableEmission() {
      m_EmissiveMaterial.DisableKeyword("_EMISSION");
    }

    private void EnableEmission() {
      m_EmissiveMaterial.EnableKeyword("_EMISSION");
    }

    private IEnumerator ToggleLight() {
      m_IsLightToggling = true;
      while (m_IsLightToggling) {
        if (m_LightIsEnabled) {
          yield return m_WaitForSeconds;
          DisableEmission();
          m_LightIsEnabled = false;
        }
        yield return m_WaitForSeconds;
        EnableEmission();
        m_LightIsEnabled = true;
      }
    }
  }
}

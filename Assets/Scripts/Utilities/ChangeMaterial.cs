using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {
  public Material material;

  public void SwapMaterial() {
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   foreach (Renderer renderer in renderers) {
      renderer.material = material;
    }
  }
}

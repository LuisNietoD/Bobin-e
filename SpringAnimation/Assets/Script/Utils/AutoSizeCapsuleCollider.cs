using UnityEngine;
using UnityEditor;

public class AutoSizeCapsuleCollider : MonoBehaviour {

    [MenuItem("My Tools/Collider/Fit to Children")]
    static void FitToChildren() {
        foreach (GameObject rootGameObject in Selection.gameObjects) {
            Collider rootCollider = rootGameObject.GetComponent<Collider>();

            if (rootCollider is BoxCollider) {
                FitBoxCollider(rootGameObject);
            } else if (rootCollider is SphereCollider) {
                FitSphereCollider(rootGameObject);
            }
        }
    }

    static void FitBoxCollider(GameObject rootGameObject) {
        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        for (int i = 0; i < rootGameObject.transform.childCount; ++i) {
            Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
            if (childRenderer != null) {
                if (hasBounds) {
                    bounds.Encapsulate(childRenderer.bounds);
                } else {
                    bounds = childRenderer.bounds;
                    hasBounds = true;
                }
            }
        }

        BoxCollider collider = (BoxCollider)rootGameObject.GetComponent<Collider>();
        collider.center = bounds.center - rootGameObject.transform.position;
        collider.size = bounds.size;
    }

    static void FitSphereCollider(GameObject rootGameObject) {
        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        for (int i = 0; i < rootGameObject.transform.childCount; ++i) {
            Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
            if (childRenderer != null) {
                if (hasBounds) {
                    bounds.Encapsulate(childRenderer.bounds);
                } else {
                    bounds = childRenderer.bounds;
                    hasBounds = true;
                }
            }
        }

        SphereCollider collider = (SphereCollider)rootGameObject.GetComponent<Collider>();
        collider.center = bounds.center - rootGameObject.transform.position;
        collider.radius = 0.35f * bounds.size.magnitude;
    }
}

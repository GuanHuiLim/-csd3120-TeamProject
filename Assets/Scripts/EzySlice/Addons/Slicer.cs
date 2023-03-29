using UnityEngine;
using EzySlice;
public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;


    private void Update()
    {
        if (isTouched == true)
        {

            Debug.Log("isTouched == true");
            isTouched = false;
            
            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(0.07f, 0.1f, 0.07f), transform.rotation, sliceMask);
            //Debug.Log("objectsToBeSliced num = " + objectsToBeSliced.Length.ToString());
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                if (objectToBeSliced.gameObject.CompareTag("Sliceable") == false)
                {
                    continue;
                }
                //Debug.Log("objectToBeSliced procesing");
                Material replacementMaterial = objectToBeSliced.gameObject.GetComponent<Renderer>().material;
                bool useGravity = objectToBeSliced.gameObject.GetComponent<Rigidbody>().useGravity;

                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, replacementMaterial);
                GameController.AddScore(10);
                GameController.sword_cut_event.Invoke();
                if (slicedObject == null)
                {
                    //Debug.Log("slicedObject == null");
                    continue;
                }
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, replacementMaterial);
                if (upperHullGameobject == null)
                    Debug.Log("upperHullGameobject == null");

                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, replacementMaterial);
                if (lowerHullGameobject == null)
                    Debug.Log("lowerHullGameobject == null");

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject, useGravity);
                MakeItPhysical(lowerHullGameobject, useGravity);

                Destroy(objectToBeSliced.gameObject);
            }
        }
    }

    private void MakeItPhysical(GameObject obj, bool useGravity)
    {
        if (obj.TryGetComponent<MeshFilter>(out MeshFilter mesh_filter) == false)
        {
            mesh_filter = obj.AddComponent<MeshFilter>();
        }
        if (obj.TryGetComponent<MeshCollider>(out MeshCollider mesh_collider) == false)
        {
            mesh_collider = obj.AddComponent<MeshCollider>();
        }
        mesh_collider.convex = true;
        if (obj.TryGetComponent<WebXR.Interactions.MouseDragObject>(out WebXR.Interactions.MouseDragObject mousedrag_obj) == false)
            obj.AddComponent<WebXR.Interactions.MouseDragObject>();
        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
            rb = obj.AddComponent<Rigidbody>();
        rb.useGravity = useGravity;
        rb.AddExplosionForce(130, obj.transform.position, 10);
        //obj.tag = "Sliceable";

        if (obj.TryGetComponent<DestroyedPieceLogic>(out DestroyedPieceLogic destroyedPieceLogic) == false)
            destroyedPieceLogic = obj.AddComponent<DestroyedPieceLogic>();
        if (obj.TryGetComponent<DestroyAfterAWhile>(out DestroyAfterAWhile destroyAfterAWhile) == false)
            destroyAfterAWhile = obj.AddComponent<DestroyAfterAWhile>();
        destroyAfterAWhile.StartFading();
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }


}
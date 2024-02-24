using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TestHasComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.gameObject.HasOrAddComponent<BoxCollider>() ? "1" : "2");

        foreach (var component in GetComponents<Component>())
        {
            if (component.GetType() == typeof(MeshRenderer))
            {
                //Debug.Log("1 : " + component);
            }

            //Debug.Log("1 : " + component);
        }

        Debug.Log(this.gameObject.GetComponents<Collider>().Count()); //GetComponentsInChildren

        //        List<Component> allComponents = new List<Component>();
        //        allComponents = gameObject.GetComponents<Component>().ToList();
        //        foreach (var component in allComponents)
        //        {
        //            //Debug.Log("2 : " + component.name);
        //        }
        //
        //        foreach (Transform transform in obj.transform)
        //        {
        //            if (transform.GetComponent<Renderer>())
        //            {
        //                Debug.Log(transform.GetComponent<Renderer>().ToString());
        //            }
        //        }
    }
}

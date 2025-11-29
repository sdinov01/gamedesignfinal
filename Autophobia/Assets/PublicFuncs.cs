using UnityEngine;

public class PublicFuncs : MonoBehaviour
{
    public void ParentIfNotNull (GameObject parent, Transform child)
    {
        if (parent != null) 
        {
            child.SetParent (parent.transform, true);
        }
        else
        {
            Debug.LogError (parent + " is not found");
        }
    }

    public Vector3 vEqual (float f)
    {
        return new Vector3 (f, f, f);
    }
}

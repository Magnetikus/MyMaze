using UnityEngine;

public class ChangeIsLook : MonoBehaviour
{
    // private bool isMapCollection = false;

    //public void SetIsMapCollectionTrue()
    //{
    //    isMapCollection = true;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Movet") || other.CompareTag("NoMovet") || other.CompareTag("Cell"))
        {
            other.GetComponent<VisibleOnn>().OnVisible();
            other.GetComponent<VisibleOnn>().OnSpriteMap();
        }
        //if (isMapCollection == false)
        //{
        //    IsLook isLook = other.GetComponent<IsLook>();
        //    if (isLook != null)
        //    {
        //        isLook.SetIsLook(true);
        //    }
           
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Movet") || other.CompareTag("NoMovet") || other.CompareTag("Cell"))
        {
            other.GetComponent<VisibleOnn>().OffVisible();
        }
        //if (isMapCollection == false && other.CompareTag("Monster"))
        //{
        //    IsLook isLook = other.GetComponent<IsLook>();
        //    if (isLook != null)
        //    {
        //        isLook.SetIsLook(false);
        //    }
        //}
    }
}

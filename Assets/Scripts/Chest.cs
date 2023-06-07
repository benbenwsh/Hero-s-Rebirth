using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] private GameObject[] collectables;

    protected bool opened = false;

    protected virtual void OpenChest()
    {
        if (!opened)
        {
            animator.SetTrigger("Open");

            int randomNo = Random.Range(0, collectables.Length);

            GameObject collectable = Instantiate(collectables[randomNo], this.transform, false) as GameObject;
            collectable.transform.localPosition = new Vector3(0, -0.5f, 0);

            //opened = true;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!collision.gameObject.GetComponent<Player>().playerIsDead)
            {
                OpenChest();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Rigidbody RB;
    public Transform[] Nums;
    public int num;

    Transform originPos;

    private void Start()
    {
        originPos.position = transform.position;
    }

    public IEnumerator Roll()
    {
        yield return null;

        transform.position = originPos.position + new Vector3(0,5f,0);
        transform.localEulerAngles = new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
        RB.angularVelocity = Random.insideUnitSphere * Random.Range(-1000, 1000);

        yield return new WaitForSeconds(3f);

        while(true)
        {
            yield return null;
            if (RB.velocity.sqrMagnitude < 0.001f) break;
        }

        for (int i = 0; i < Nums.Length; i++)
        {
            if(Nums[i].position.y > 2)
            {
                num = i + 1;
                break;
            }
        }


    }
}

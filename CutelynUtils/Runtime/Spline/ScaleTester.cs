using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaitUtils
{
    public class ScaleTester : MonoBehaviour
    {
        public void decreaseScale() {

            transform.localScale -= Vector3.one * Time.deltaTime;
        }
    }
}

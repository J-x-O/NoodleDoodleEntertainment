using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Freezable : MonoBehaviour
    {
        private bool frozen = false;
        private float frozenTime = 0;
        public GameObject ice;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(frozen) {
                frozenTime -= Time.deltaTime;
                if (frozenTime <= 0)
                {
                    frozen = false;
                    ice.SetActive(false);
                }
                
            } 
        }

        public void freeze(float duration) {
            frozen = true;
            frozenTime = duration;
            ice.SetActive(true);
        }
    }
}

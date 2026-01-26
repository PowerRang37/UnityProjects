using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmniumLessons
{
    public class CreativeScript : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 35f;
        
        private void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            
            Vector3 rotation = new Vector3(moveHorizontal, 0.0f, moveVertical);
            
            transform.Rotate(rotation * _rotationSpeed * Time.deltaTime);
        }
    }
}
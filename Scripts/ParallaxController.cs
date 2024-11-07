
using UnityEngine;

namespace Bombdrop
{
    public class ParallaxController : MonoBehaviour
    {
        
       [SerializeField] Transform[] backgrounds; //Array of background layers
       [SerializeField] float smoothing = 10f; //how smooth the paralax effect is
       [SerializeField] float multiplier = 15f; //how much the parallax effect increments per layer

       Transform cam; //reference to the main camera
       Vector3 previousCamPos; //position of the cam in the previous frame

       void Awake() => cam = Camera.main.transform;

       void Start() => previousCamPos = cam.position;

       void Update() {
        //iterate through each background layer
        for(var i = 0; i< backgrounds.Length; i++){
            var parallax = (previousCamPos.y - cam.position.y) * (i * multiplier);
            var targetY = backgrounds[i].position.y + parallax;

            var targetPosition = new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);

        }

        previousCamPos = cam.position;
       }
    }
}
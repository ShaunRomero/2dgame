
using System;
using UnityEngine;

namespace Bombdrop {
    // Defines a Projectile class within the Bombdrop namespace
    public class Projectile : MonoBehaviour {
        // Variables for the projectile's speed, muzzle effect, and hit effect
        [SerializeField] float speed;                 // Controls the speed of the projectile
        [SerializeField] GameObject muzzlePrefab;     // Prefab for muzzle effect when the projectile is launched
        [SerializeField] GameObject hitPrefab;        // Prefab for hit effect when the projectile collides with something

        Transform parent;                             // Parent transform to manage hierarchy
        
        // Sets the projectile's speed
        public void SetSpeed(float speed) => this.speed = speed;

        // Sets the parent of the projectile (used for organizing hierarchy in the scene)
        public void SetParent(Transform parent) => this.parent = parent;

        // Delegate that can be used to trigger additional actions when the projectile moves
        public Action Callback;
        
        // Start is called before the first frame update
        void Start() {
            // Instantiate and play muzzle effect at the projectile's initial position
            if (muzzlePrefab != null) {
                var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity); // Creates muzzle effect
                muzzleVFX.transform.forward = gameObject.transform.forward;                          // Aligns effect with projectile direction
                muzzleVFX.transform.SetParent(parent);                                              // Sets the parent of the effect to keep it organized
                
                //DestroyParticleSystem(muzzleVFX); // Schedules the effect to be destroyed once finished
            }
        }
        
        // Update is called once per frame
        void Update() {
            transform.SetParent(null); // Detaches the projectile from any parent, if it had one
            transform.position += transform.forward * (speed * Time.deltaTime); // Moves the projectile forward based on speed and time
            
            Callback?.Invoke(); // Invokes Callback if it is set, allowing for additional actions during movement
        }

        // Called when the projectile collides with another object
        void OnCollisionEnter(Collision collision) {
            // Creates and plays hit effect at collision point if hitPrefab is assigned
            if (hitPrefab != null) {
                ContactPoint contact = collision.contacts[0]; // Gets the contact point of collision
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity); // Creates hit effect
                
                DestroyParticleSystem(hitVFX); // Schedules the hit effect to be destroyed after it finishes
            }
            
            // Checks if the object hit has a Plane component and applies damage if it does
        //     var plane = collision.gameObject.GetComponent<Plane>();
        //     if (plane != null) {
        //         plane.TakeDamage(10); // Inflicts 10 points of damage to the plane
        //     }

        //     Destroy(gameObject); // Destroys the projectile itself after collision
        // }
        
        // Method to destroy a particle system once it has finished playing
        void DestroyParticleSystem(GameObject vfx) {
            // Retrieves the ParticleSystem component on the GameObject or its children
            var ps = vfx.GetComponent<ParticleSystem>();
            if (ps == null) {
                ps = vfx.GetComponentInChildren<ParticleSystem>();
            }
            // Destroys the effect after the duration of the particle system
            Destroy(vfx, ps.main.duration);
        }
    }
}}

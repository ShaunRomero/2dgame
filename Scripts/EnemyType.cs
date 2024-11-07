using UnityEngine;

namespace Bombdrop {
    [CreateAssetMenu(fileName = "EnemyType", menuName = "Bombdrop/EnemyType", order = 0)]
    public class EnemyType : ScriptableObject {
        public GameObject enemyPrefab;
        public GameObject weaponPrefab;
        public float speed = 2f;
    }
}
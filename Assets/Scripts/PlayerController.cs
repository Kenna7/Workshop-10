// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f; // Default speed sensitivity
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject planePrefab;


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * (this.speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * (this.speed * Time.deltaTime));
        
        // Use the "down" variant to avoid spamming projectiles. Will only get
        // triggered on the frame where the key is initially pressed.
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0,0,0));
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Vector3.down;
            float distance;
            // Vector3 realMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (plane.Raycast(ray, out distance)){
                worldPosition = ray.GetPoint(distance);
            }
            Vector3 projectileDir = worldPosition - gameObject.transform.position;
            var projectile = Instantiate(this.projectilePrefab);
            projectile.GetComponent<ProjectileController>().SetVelocity(projectileDir);
            projectile.transform.position = gameObject.transform.position;
        }
    }
}

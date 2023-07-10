using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform segmentsPrefab;
    private Vector2 direction;

    private List<Transform> segments;

    private void Start() {
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W)){
            direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            direction = Vector2.right;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            direction = Vector2.down;
        }
    }

    private void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0f
        );
    }

    private void Grow() {
        Transform segment = Instantiate(segmentsPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void ResetState() {
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Food")) {
            Grow();
        } else if (other.gameObject.CompareTag("Obstacle")) {
            Debug.Log("iiiii");
            ResetState();
        }
    }
}

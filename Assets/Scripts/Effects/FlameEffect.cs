using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour {
    Animator animator;
    ParticleSystem particleSystem;
	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        //particleSystem.Stop();
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.2f, 1f);
        animator.Play("Flame", -1, Random.Range(0f, 1f));
	}
}

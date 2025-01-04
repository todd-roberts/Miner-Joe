using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private int _id;
    private Key _requiredKey;
    [SerializeField] private int _targetSceneIndex; // Scene index selected in the dropdown
    [SerializeField] private Animator doorAnimator; // Animator for the door
    [SerializeField] private AudioClip openSound; // Sound to play when opening the door

    public int GetTargetSceneIndex() => _targetSceneIndex;

    public void SetTargetSceneIndex(int index) => _targetSceneIndex = index;

    public int GetId() => _id;

    private void Awake()
    {
        _requiredKey = GetComponentInChildren<Key>();

        if (_requiredKey != null) _requiredKey.Init(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Miner miner = other.GetComponent<Miner>();

        if (miner == null) return;

        miner.TryOpenDoor(this);
    }

    public IEnumerator Open()
    {
        // Play open animation
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        // Play sound
        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, transform.position);
        }

        // Wait for the animation to finish
        if (doorAnimator != null)
        {
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            while (stateInfo.normalizedTime < 1.0f && !stateInfo.IsName("Open"))
            {
                yield return null;
                stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            }
        }

        // Load the target scene
        SceneManager.LoadScene(_targetSceneIndex);
    }

    public static Door GetDoorById(int id)
    {
        return FindObjectsOfType<Door>().FirstOrDefault(door => door.GetId() == id);
    }
}

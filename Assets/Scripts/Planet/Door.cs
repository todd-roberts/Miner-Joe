using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private int _id;
    private Key _requiredKey;
    [SerializeField] private bool _isReturnDoor;
    [SerializeField] private int _targetSceneIndex;
    [SerializeField] private AudioClip _openSound;

    public int GetTargetSceneIndex() => _targetSceneIndex;

    public void SetTargetSceneIndex(int index) => _targetSceneIndex = index;

    public int GetId() => _id;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _requiredKey = GetComponentInChildren<Key>();
        InitKey();
    }

    private void InitKey()
    {
       if (_requiredKey == null) return;

       _requiredKey.gameObject.SetActive(!_isReturnDoor);
    }

    public bool IsReturnDoor() => _isReturnDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Miner miner = other.GetComponent<Miner>();

        if (miner == null) return;

        miner.TryOpenDoor(this);
    }

    public void Open() {
        StartCoroutine(OpenRoutine());
    }

    public IEnumerator OpenRoutine()
    {

        _animator.SetTrigger("Open");

        _audioSource.PlayOneShot(_openSound);
       

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        
        while (!stateInfo.IsName("Open"))
        {
            yield return null;
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        while (stateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        SceneManager.ChangeScene(_targetSceneIndex);
    }

    public static Door GetDoorById(int id)
    {
        return FindObjectsOfType<Door>().FirstOrDefault(door => door.GetId() == id);
    }
}

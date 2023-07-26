using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace VSExtension.Nodes
{
    public abstract class MonoBehaviourUnit : Unit
    {
        public GameObject gameObject;
        public MonoBehaviour monoBehaviour;
        public Transform transform;

        #region Unity Methods

        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        public T GetComponentInChildren<T>() where T : Component
        {
            return gameObject.GetComponentInChildren<T>();
        }

        public T GetComponentInParent<T>() where T : Component
        {
            return gameObject.GetComponentInParent<T>();
        }

        public T[] GetComponents<T>() where T : Component
        {
            return gameObject.GetComponents<T>();
        }

        public T[] GetComponentsInChildren<T>() where T : Component
        {
            return gameObject.GetComponentsInChildren<T>();
        }

        public T[] GetComponentsInParent<T>() where T : Component
        {
            return gameObject.GetComponentsInParent<T>();
        }

        public void StartCoroutine(IEnumerator routine)
        {
            monoBehaviour.StartCoroutine(routine);
        }

        public void StopCoroutine(IEnumerator routine)
        {
            monoBehaviour.StopCoroutine(routine);
        }
        
        public void Invoke(string methodName, float time)
        {
            monoBehaviour.Invoke(methodName, time);
        }
        
        public void InvokeRepeating(string methodName, float time, float repeatRate)
        {
            monoBehaviour.InvokeRepeating(methodName, time, repeatRate);
        }
        
        public virtual void Awake()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }
        
        public virtual void LateUpdate()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnEnable()
        {
            StartCoroutine(UpdateCoroutine());
            StartCoroutine(FixedUpdateCoroutine());
        }
        
        #endregion

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            gameObject = instance.gameObject;
            transform = gameObject.transform;
            monoBehaviour = gameObject.GetComponent<MonoBehaviour>();

            Awake();
            OnEnableCoroutine();
        }
        
        private IEnumerator FixedUpdateCoroutine()
        {
            var time = new WaitForSeconds(Time.fixedDeltaTime);
            while (gameObject.activeSelf)
            {
                yield return time;
                FixedUpdate();
            }
        }

        private IEnumerator UpdateCoroutine()
        {
            while (gameObject.activeSelf)
            {
                Update();
                yield return null;
                LateUpdate();
            }

            OnDisable();
        }

        private async void OnEnableCoroutine()
        {
            while (Application.isPlaying)
            {
                OnEnable();

                while (gameObject.activeSelf)
                {
                    if (!Application.isPlaying)
                        return;
                    await Task.Delay(100);
                }

                await OnEnableTask();
                if (!Application.isPlaying)
                    return;
            }
        }

        private async Task OnEnableTask()
        {
            while (!gameObject.activeSelf && Application.isPlaying)
                await Task.Delay(100);
        }
    }
}
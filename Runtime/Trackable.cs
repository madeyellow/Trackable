using UnityEngine;
using UnityEngine.Events;

namespace MadeYellow.Trackables
{
    /// <summary>
    /// Compact generic property change tracker
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class Trackable<T>
    {
        /// <summary>
        /// Determines how change tracking should happen
        /// </summary>
        [SerializeField]
        private TrackingStrategy _trackingStrategy = TrackingStrategy.Auto;

        #region Values
        private T _value;

        /// <summary>
        /// Current value that will be
        /// </summary>
        public T value { get => _value; set => Update(@value); }



        /// <summary>
        /// Previous value of <see cref="value"/>
        /// </summary>
        public T previousValue { get; private set; } =  default;


        /// <summary>
        /// Value of <see cref="Time.time"/> when <see cref="previousValue"/> get updated last time
        /// </summary>
        public float valueTimestamp { get; private set; }
        #endregion

        #region Events
        [Space]
        [Header("Events")]
        [SerializeField]
        private UnityEvent _onValueChanged = new UnityEvent();

        /// <summary>
        /// Invokes on <see cref="Commit"/> call if <see cref="value"/> differs from <see cref="previousValue"/>
        /// </summary>
        public UnityEvent onValueChanged => _onValueChanged;
        #endregion

        #region Constructors
        public Trackable(TrackingStrategy trackingStrategy = TrackingStrategy.Auto)
        {
            _value = default;
            previousValue = default;

            _trackingStrategy = trackingStrategy;
        }

        public Trackable(T value, TrackingStrategy trackingStrategy = TrackingStrategy.Auto)
        {
            _value = value;
            previousValue = value;

            _trackingStrategy = trackingStrategy;
        }
        #endregion

        private void Update(T newValue)
        {
            _value = newValue;

            if (_trackingStrategy == TrackingStrategy.Auto)
            {
                Commit();
            }
        }

        /// <summary>
        /// Performs change tracking & updates previousValue if needed
        /// </summary>
        public void Commit()
        {
            if ((previousValue == null && value != null) || (previousValue != null && !previousValue.Equals(value)))
            {
                valueTimestamp = Time.time;

                _onValueChanged.Invoke();
            }

            previousValue = value;
        }
    }
}
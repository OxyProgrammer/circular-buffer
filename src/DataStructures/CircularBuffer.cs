using System.Collections;

namespace CircularBufferDemo.DataStructures
{
    /// <summary>
    /// Represents a circular buffer of a fixed size that allows overwriting when full.
    /// </summary>
    /// <typeparam name="T">The type of elements in the buffer.</typeparam>
    public class CircularBuffer<T> : IEnumerable<T> 
    {
        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the buffer.
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// Gets the total number of elements the buffer can hold.
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// Gets a value indicating whether the buffer is at full capacity.
        /// </summary>
        public bool IsFull => _size == _capacity;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CircularBuffer class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The maximum number of elements the buffer can hold.</param>
        /// <exception cref="ArgumentException">Thrown when capacity is not positive.</exception>
        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be positive.", nameof(capacity));
            }

            _capacity = capacity;
            _buffer = new T[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets the item at the specified index in the buffer.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get.</param>
        /// <returns>The item at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                {
                    throw new IndexOutOfRangeException();
                }

                int actualIndex = (_tail + index) % _capacity;
                return _buffer[actualIndex];
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the buffer.
        /// </summary>
        /// <param name="item">The item to add to the buffer.</param>
        public void Add(T item)
        {
            if (_size == _capacity)
            {
                _buffer[_tail] = item;
                _tail = (_tail + 1) % _capacity;
                _head = _tail;
            }
            else
            {
                _buffer[_head] = item;
                _head = (_head + 1) % _capacity;
                _size++;
            }
        }

        /// <summary>
        /// Removes all items from the buffer.
        /// </summary>
        public void Clear()
        {
            _head = 0;
            _tail = 0;
            _size = 0;
            Array.Clear(_buffer, 0, _capacity);
        }

        /// <summary>
        /// Determines whether the buffer contains a specific value.
        /// </summary>
        /// <param name="item">The value to locate in the buffer.</param>
        /// <returns>true if the buffer contains the specified item; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return Array.IndexOf(_buffer, item) != -1;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the buffer.
        /// </summary>
        /// <returns>An IEnumerator&lt;T&gt; that can be used to iterate through the buffer.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                yield return this[i];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the buffer.
        /// </summary>
        /// <returns>An IEnumerator that can be used to iterate through the buffer.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private Variables

        private T[] _buffer;
        private int _head;
        private int _tail;
        private int _size;
        private readonly int _capacity;

        #endregion
    }
}



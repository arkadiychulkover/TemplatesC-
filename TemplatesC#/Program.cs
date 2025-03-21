namespace TemplatesC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            int y = 1;
            Console.WriteLine($"x: {x}, y: {y}");
            Swap(ref x, ref y);
            Console.WriteLine($"x: {x}, y: {y}");

            PrQueue<int> pq = new PrQueue<int>();
            pq.AddNode(1, 1);
            pq.AddNode(2, 2);
            pq.AddNode(3, 3);
            pq.AddNode(4, 4);
            pq.AddNode(5, 5);
            pq.IncreaseKey(3, 6);
            pq.Del();
            pq.Show();

            LList<int> list = new LList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.DelEl(2);
            list.Show();

            DList<int> dlist = new DList<int>();
            dlist.AddToEnd(1);
            dlist.AddToEnd(2);
            dlist.AddToEnd(3);
            dlist.AddToEnd(4);
            dlist.AddToEnd(5);
            //dlist.DelEl(3);
            dlist.Del();
            dlist.Show();

            string[] arr = { "a", "b", "c", "d", "e" };
            string[] arr1 = { "a", "b", "c", "d" };
            int[] arr2 = { 1, 2, 3, 4, 5 };
            int[] arr3 = { 1, 2, 3, 4 };
            Console.WriteLine(FindMedian(arr));
            Console.WriteLine(FindMedian(arr1));
            Console.WriteLine(FindMedian(arr2));
            Console.WriteLine(FindMedian(arr3));
        }
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        public static T FindMedian<T>(T[] arr) where T : IComparable<T>// колекции не прошли
        {
            if (arr == null || arr.Length == 0)
                throw new ArgumentException("empty");

            Array.Sort(arr);
            int n = arr.Length;

            if (n % 2 == 1)
            {
                return arr[n / 2];
            }
            else
            {
                if (typeof(T) == typeof(string))
                {
                    return arr[n / 2 - 1];
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(decimal))
                {
                    dynamic first = arr[n / 2 - 1];
                    dynamic second = arr[n / 2];
                    return (T)((first + second) / 2);
                }
                else
                {
                    throw new InvalidOperationException("inv type");
                }
            }
        }
    }
    class PrQueue<T>
    {
        private class Node
        {
            public T Data;
            public int Priority;
            public Node Next;
        }

        private Node head;

        public PrQueue()
        {
            head = null;
        }

        public void AddNode(T item, int priority)
        {
            Node newNode = new Node { Data = item, Priority = priority, Next = null };

            if (head == null || priority < head.Priority)
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null && current.Next.Priority <= priority)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
            }
        }
        public T Maximum()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
            }
            return head.Data;
        }
        public void IncreaseKey(T item, int newPriority)
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            Node prev = null, current = head, targetNode = null, targetPrev = null;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, item))//не хотел перевантаживать == поетому посмотрел в инете
                {
                    targetNode = current;
                    targetPrev = prev;
                    break;
                }
                prev = current;
                current = current.Next;
            }

            if (targetNode == null)
            {
                Console.WriteLine("not found");
                return;
            }

            if (targetPrev != null)
                targetPrev.Next = targetNode.Next;
            else
                head = targetNode.Next;

            AddNode(item, newPriority);
        }
        public T Del()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
            }

            T item = head.Data;
            head = head.Next;
            return item;
        }
        public bool IsEmpty()
        {
            return head == null;
        }
        public int Count()
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
        public void Show()
        {
            Node current = head;
            while (current != null)
            {
                Console.Write($" [Data: {current.Data}, Prior: {current.Priority}]  ->");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
    }
    public class LList<T>
    {
        private class Node
        {
            public T Data;
            public Node Next;

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }
        private Node head;

        public void Add(T item)
        {
            Node newNode = new Node(item);
            newNode.Next = head;
            head = newNode;
        }
        public void AddEnd(T item)
        {
            Node newNode = new Node(item);

            if (head == null)
            {
                head = newNode;
                return;
            }

            Node temp = head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = newNode;
        }
        public void AddAfter(T existingItem, T newItem)
        {
            Node temp = head;

            while (temp != null && !EqualityComparer<T>.Default.Equals(temp.Data, existingItem))// тоже самое не хотел перегружать == поэтому посмотрел в инете
            {
                temp = temp.Next;
            }

            if (temp == null)
            {
                Console.WriteLine("not found");
                return;
            }

            Node newNode = new Node(newItem);
            newNode.Next = temp.Next;
            temp.Next = newNode;
        }
        public void Del()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }
            head = head.Next;
        }
        public void DelEnd()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            if (head.Next == null)
            {
                head = null;
                return;
            }

            Node temp1 = head, temp2 = null;
            while (temp1.Next != null)
            {
                temp2 = temp1;
                temp1 = temp1.Next;
            }
            temp2.Next = null;
        }
        public void DelEl(T item)
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            if (EqualityComparer<T>.Default.Equals(head.Data, item))
            {
                head = head.Next;
                return;
            }

            Node temp1 = head, temp2 = null;
            while (temp1 != null && !EqualityComparer<T>.Default.Equals(temp1.Data, item))
            {
                temp2 = temp1;
                temp1 = temp1.Next;
            }

            if (temp1 == null)
            {
                Console.WriteLine("empty");
                return;
            }

            temp2.Next = temp1.Next;
        }
        public void Show()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            Node current = head;
            while (current != null)
            {
                Console.Write($"[{current.Data}] -> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
    }
    public class DList<T>
    {
        private class Node
        {
            public T Data;
            public Node Next;
            public Node Prev;

            public Node(T data)
            {
                Data = data;
                Next = null;
                Prev = null;
            }
        }

        private Node head;
        private Node tail;

        public void Add(T item)
        {
            Node newNode = new Node(item);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
        }
        public void AddToEnd(T item)
        {
            Node newNode = new Node(item);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
        }
        public void AddAfter(T target, T item)
        {
            Node temp = head;
            while (temp != null && !temp.Data.Equals(target))
            {
                temp = temp.Next;
            }

            if (temp == null)
            {
                Console.WriteLine("not found");
                return;
            }

            Node newNode = new Node(item);
            newNode.Next = temp.Next;
            newNode.Prev = temp;
            if (temp.Next != null)
            {
                temp.Next.Prev = newNode;
            }
            temp.Next = newNode;
            if (temp == tail)
            {
                tail = newNode;
            }
        }
        public void Del()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            head = head.Next;
            if (head != null)
                head.Prev = null;
            else
                tail = null;
        }
        public void DelEnd()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            tail = tail.Prev;
            if (tail != null)
                tail.Next = null;
            else
                head = null;
        }
        public void DelEl(T item)
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }
            if (EqualityComparer<T>.Default.Equals(head.Data, item))
            {
                head = head.Next;
                if (head != null)
                    head.Prev = null;
                else
                    tail = null;
                return;
            }
            Node temp = head;
            while (temp != null && !EqualityComparer<T>.Default.Equals(temp.Data, item))
            {
                temp = temp.Next;
            }
            if (temp == null)
            {
                Console.WriteLine("not found");
                return;
            }
            if (temp == tail)
            {
                tail = tail.Prev;
                if (tail != null)
                    tail.Next = null;
                else
                    head = null;
            }
            else
            {
                temp.Prev.Next = temp.Next;
                temp.Next.Prev = temp.Prev;
            }
        }
        public void Show()
        {
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            Node current = head;
            Console.Write("null <-");
            while (current != null)
            {
                Console.Write($" [{current.Data}] <->");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
        public void ShowReverse()
        {
            if (tail == null)
            {
                Console.WriteLine("empty");
                return;
            }

            Node current = tail;

            Console.Write("null <-");
            while (current != null)
            {
                Console.Write($" [{current.Data}] <->");
                current = current.Prev;
            }
            Console.WriteLine("null");
        }//по приколу добавил
    }
}

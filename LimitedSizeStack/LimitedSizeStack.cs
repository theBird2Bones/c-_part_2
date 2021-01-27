using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoApplication
{
    public class StackItem<T>{
        public T Value{ get; set; }
        public StackItem<T> Next{ get; set; }
        public StackItem<T> Previous{ get; set; }
    }
    
    public class LimitedSizeStack<T>{
        private int limit;
        private int length = 0;
        private StackItem<T> head = null;
        private StackItem<T> tail = null;
        public LimitedSizeStack(int limit){
            this.limit = limit;
        }

        public void Push(T item){
            if (head == null && limit > 0) {
                head  = new StackItem<T>(){Value = item, Next = null, Previous = null};
                tail = head;
                length++;
            }
            else if(head != null && limit > 0){
                var newItem = new StackItem<T>(){Value = item, Next = null, Previous = tail};
                tail.Next = newItem;
                tail = newItem;
                length++;
                if (length > limit) {
                    head = head.Next;
                    head.Previous = null;
                    length--;
                }
            }
        }

        public T Pop(){
            if (tail == null) throw new InvalidOperationException();
            var res = tail.Value;
            if (tail.Previous == null) {
                tail = null;
                head = tail;
            }
            else {
                tail = tail.Previous;
                tail.Next = null;
            }
            length--;
            
            return res;
        }

        public int Count{
            get{ return length; }
        } 
    }
}

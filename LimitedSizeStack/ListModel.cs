using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TodoApplication
{
    public class ChangedItem<TItem>{
        public int Index{ get;  set; }
        public TItem Item{ get; set; }
        public char OperationWasMade{ get; set; }
    }
    
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        private LimitedSizeStack<ChangedItem<TItem>> StackToUndo;
        

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            StackToUndo = new LimitedSizeStack<ChangedItem<TItem>>(Limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            StackToUndo.Push(new ChangedItem<TItem>(){Index = Items.Count-1, Item = item,OperationWasMade = '+'});
        }

        public void RemoveItem(int index)
        {
            StackToUndo.Push(new ChangedItem<TItem>(){Index = index, Item = Items[index],OperationWasMade = '-'});
            Items.RemoveAt(index);
        }

        public bool CanUndo(){
            return StackToUndo.Count != 0;
        }

        public void Undo(){
            var lastOperation = StackToUndo.Pop();
            if (lastOperation.OperationWasMade == '+')
                Items.RemoveAt(lastOperation.Index);
            if (lastOperation.OperationWasMade == '-')
                Items.Insert(lastOperation.Index,lastOperation.Item);
        }
    }
}

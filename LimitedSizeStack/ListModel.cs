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
        private LimitedSizeStack<ChangedItem<TItem>> HistoryOfChanges;
        

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            HistoryOfChanges = new LimitedSizeStack<ChangedItem<TItem>>(Limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            HistoryOfChanges.Push(new ChangedItem<TItem>(){Index = Items.Count-1, Item = item,OperationWasMade = '+'});
        }

        public void RemoveItem(int index)
        {
            HistoryOfChanges.Push(new ChangedItem<TItem>(){Index = index, Item = Items[index],OperationWasMade = '-'});
            Items.RemoveAt(index);
        }

        public bool CanUndo(){
            return HistoryOfChanges.Count != 0;
        }

        public void Undo(){
            var lastChange = HistoryOfChanges.Pop();
            if (lastChange.OperationWasMade == '+')
                Items.RemoveAt(lastChange.Index);
            if (lastChange.OperationWasMade == '-')
                Items.Insert(lastChange.Index,lastChange.Item);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_Lab1
{
    public delegate void ModifyItemsInStructure(DirectoryItem structureItem);

    public class DirectoryItem : StructureItem
    {
        public event ModifyItemsInStructure RefreshChildItems;

        private bool _isChanged;
        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
            set
            {
                _isChanged = value;

                if (_isChanged)
                {
                    RefreshChildItems?.Invoke(this);
                }
            }
        }
        public ObservableCollection<StructureItem> Items { get; set; }

        public DirectoryItem(string title, string path, ModifyItemsInStructure modifyItemsInStructure) : base(title, path)
        {
            Items = new ObservableCollection<StructureItem>();

            RefreshChildItems += modifyItemsInStructure;
        }

        public void AddNewItem(StructureItem item)
        {
            Items.Add(item);
        }

        public void ClearItemsCollection()
        {
            Items.Clear();
        }

        public override string ToString()
        {
            return $"{Title} [{Items.Count}]";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdvancedTaskManager
{
    public class ListViewColumnSorter : IComparer
    {
        private int col;
        public int ColumnToSort
        {
            set { this.col = value; }
            get { return this.col; }
        }
        private SortOrder order;
        public SortOrder OrderOfSort
        {
            set { this.order = value; }
            get { return this.order; }
        }
        private string ToString(string txt)
        {
            if (txt == null || txt == string.Empty)
                return "0";
            else
                return txt;
        }
        public ListViewColumnSorter()
        {
            ColumnToSort = 0;
            OrderOfSort = SortOrder.None;
        }
        public ListViewColumnSorter(int coltosort, SortOrder orderofsort)
        {
            ColumnToSort = coltosort;
            OrderOfSort = orderofsort;
        }
        public int Compare(object x, object y)
        {
            ListViewItem l1 = (ListViewItem)x;
            ListViewItem l2 = (ListViewItem)y;

            if (l1.ListView.Columns[col].Tag == null)
            {
                string str1 = l1.SubItems[col].Text;
                string str2 = l2.SubItems[col].Text;

                if (order == SortOrder.Ascending)
                {
                    return str1.CompareTo(str2);
                }
                else
                {
                    return str2.CompareTo(str1);
                }
            }
            else if (l1.ListView.Columns[col].Tag.ToString() == "INT")
            {
                int fl1 = int.Parse(ToString(l1.SubItems[col].Text));
                int fl2 = int.Parse(ToString(l2.SubItems[col].Text));

                if (order == SortOrder.Ascending)
                {
                    return fl1.CompareTo(fl2);
                }
                else
                {
                    return fl2.CompareTo(fl1);
                }
            }
            else if (l1.ListView.Columns[col].Tag.ToString() == "INT K")
            {
                string str1 = l1.SubItems[col].Text.Replace(" K", "");
                string str2 = l2.SubItems[col].Text.Replace(" K", "");
                int i1 = int.Parse(str1.Trim());
                int i2 = int.Parse(str2.Trim());
                if (order == SortOrder.Ascending)
                {
                    return i1.CompareTo(i2);
                }
                else
                {
                    return i2.CompareTo(i1);
                }
            }
            else if (l1.ListView.Columns[col].Tag.ToString() == "TIME")
            {
                TimeSpan t1 = DateTime.Parse(l1.SubItems[col].Text).TimeOfDay;
                TimeSpan t2 = DateTime.Parse(l2.SubItems[col].Text).TimeOfDay;
                if (order == SortOrder.Ascending)
                {
                    return t1.CompareTo(t2);
                }
                else
                {
                    return t2.CompareTo(t1);
                }
            }
            else if (l1.ListView.Columns[col].Tag.ToString() == "TIMEMS")
            {
                TimeSpan t1 = TimeSpan.Parse(l1.SubItems[col].Text);
                TimeSpan t2 = TimeSpan.Parse(l2.SubItems[col].Text);
                if (order == SortOrder.Ascending)
                {
                    return t1.CompareTo(t2);
                }
                else
                {
                    return t2.CompareTo(t1);
                }
            }
            else if (l1.ListView.Columns[col].Tag.ToString() == "DECIMAL")
            {
                string item1 = l1.SubItems[col].Text;
                string item2 = l2.SubItems[col].Text;
                item1 = item1.Replace('%', '\0');
                item1 = item1.Replace(" MB", "");
                item1 = item1.Replace(" MB/s", "");
                item1 = item1.Replace(" Mbps", "");

                item2 = item2.Replace('%', '\0');
                item2 = item2.Replace(" MB", "");
                item2 = item2.Replace(" MB/s", "");
                item2 = item2.Replace(" Mbps", "");

                double d1, d2;
                if (item1.Equals(""))
                    d1 = 0.0;
                else
                    d1 = Double.Parse(item1);

                if (item2.Equals(""))
                    d2 = 0.0;
                else
                    d2 = Double.Parse(item2);

                if (order == SortOrder.Ascending)
                {
                    return d1.CompareTo(d2);
                }
                else
                {
                    return d2.CompareTo(d1);
                }
            }
            else
            {
                string str1 = l1.SubItems[col].Text;
                string str2 = l2.SubItems[col].Text;

                if (order == SortOrder.Ascending)
                {
                    return str1.CompareTo(str2);
                }
                else
                {
                    return str2.CompareTo(str1);
                }
            }
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlLanguageImplementation
{
    [Serializable]
    class Table
    {
        public String name; // table name
        public int columnNr; // columns number
        public  Dictionary<String, String> columnsNameTypeList; // a list of types and names of the table's colums
        public int entryNr;  // an entrys counter to help with the keys of the entryes
        public  Dictionary<int, String> tableEntryList;    // the entryes list of the type[ key, value]
        public int pkFlag;

        public Table()
        {

        }

        //constructor
        public Table(String name, int colNr,int pkFlag, Dictionary<String, String> cols)
        {
            this.name = name;
            this.columnNr = colNr;
            this.columnsNameTypeList = cols;
            this.entryNr = 0;
            this.pkFlag = pkFlag;
            this.tableEntryList = new Dictionary<int, String>();   
        }

        // adds an entry
        public void addEntry(String entry)
        {
            this.entryNr = tableEntryList.Count() + 1;         
            tableEntryList.Add(entryNr, entry);

        }

        // returns the table's content
        public Dictionary<int,String> getEntries()
        {
            return this.tableEntryList;
        }

        //updates an entry using its key
        public void updateEntry(int key, String entry)
        {
            tableEntryList[key] = entry;
        }

        // removes an entry with the key provided
        public void removeEntry(int key)
        {
            tableEntryList.Remove(key);           
        }
        
        public string toString()
        {
            string str = "Table name: " + this.name + "\n Columns: \n ";
            int index = 0;
            foreach (var v in this.columnsNameTypeList)
            {
                index++;
                if (index == pkFlag)
                {
                    str = str + v.Key + " ; " + v.Value + " (pk) " + " \n ";
                }
                else
                {
                    str = str + v.Key + " ; " + v.Value + " \n ";
                }
            }
            return str;
        }
    }


}

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

        public Table()
        {

        }

        //constructor
        public Table(String name, int colNr, Dictionary<String, String> cols)
        {
            this.name = name;
            this.columnNr = colNr;
            this.columnsNameTypeList = cols;
            this.entryNr = 0;
            this.tableEntryList = new Dictionary<int, String>();
            

        }

        // adds an entry
        public void addEntry(String entry)
        {
            this.entryNr++;          
            tableEntryList.Add(entryNr, entry);
            updateXml();

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
            updateXml();
        }

        // removes an entry with the key provided
        public void removeEntry(int key)
        {
            tableEntryList.Remove(key);
            updateXml();
        }
        
        public string toString()
        {
            string str = "Table name: " + this.name + "\n Columns: \n ";
            foreach(var v in this.columnsNameTypeList)
            {
                str = str + v.Key + ";" + v.Value + " \n ";
            }
            return str;
        }



        public void updateXml()
        {
            //todo update dupa fiecare operatie
        }
         
        public void writeToMetadata()
        {
            //todo cand se creeaza tabelu sa scrie in metadata
        }
        public void deleteFromMetadata()
        {
            //todo cand se sterge tabelu sa stearga din metadata
        }

    }


}

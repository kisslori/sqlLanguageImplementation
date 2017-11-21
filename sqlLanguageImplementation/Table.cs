using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlLanguageImplementation
{
    class Table
    {
        public String name;
        public int columnNr;
        public  Dictionary<String, String> columnsNameTypeList;
        public int entryNr;
        public  Dictionary<int, String> tableEntryList;    

        public Table()
        {

        }
        public Table(String name, int colNr, Dictionary<String, String> cols)
        {
            this.name = name;
            this.columnNr = colNr;
            this.columnsNameTypeList = cols;
            this.entryNr = 0;
            this.tableEntryList = new Dictionary<int, String>();
            

        }

        public void addEntry(String entry)
        {
            this.entryNr++;          
            tableEntryList.Add(entryNr, entry);
            updateXml();

        }

        public Dictionary<int,String> getEntries()
        {
            return this.tableEntryList;
        }

        public void updateEntry(int key, String entry)
        {
            tableEntryList[key] = entry;
            updateXml();
        }
        public void removeEntry(int key)
        {
            tableEntryList.Remove(key);
            updateXml();
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

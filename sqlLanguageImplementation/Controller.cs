using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace sqlLanguageImplementation
{
    class Controller
    {
        
        String metadata;
        String tableEntries;
        List<Table> tableList;

        //======constructors=====

        public Controller(String metadata,String tables)
        {
            this.metadata = metadata;
            this.tableEntries = tables;
            tableList = new List<Table>();
        }
        public Controller( List<Table> tables, String metadata, String tablesEntries)
        {
            this.metadata = metadata;
            this.tableEntries = tablesEntries;
            this.tableList = tables;
        }

       
        //=======methods========
        //prints the table names
        public void showTableList()
        {
            
            try
            {
                //List<Table> list = new List<Table>();
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(metadata);
                //XmlNodeList tableNodes = xmlDoc.SelectNodes("//tables/table");
                //foreach (XmlNode tableNode in tableNodes)
                //{
                //    String name = tableNode.Attributes["name"].Value;
                //    //tableNode.Attributes["age"].Value = (age + 1).ToString();
                //}
                //xmlDoc.Save("test-doc.xml");

                int i = 0;
                foreach (var t in tableList)
                {
                    i++;
                    Console.WriteLine(i+". "+t.name);//verifica daca mere bine;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };             
        }

        //prints the table names and columns
        public void showTableListDetailed()
        {
            try
            {
                int i = 0;
                foreach (var t in tableList)
                {
                    i++;
                    Console.WriteLine(i+". "+t.toString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }
        // prints the entries from that table
        public void showTableContent(int tableNr)
        {
            try
            {
                foreach (var e in tableList[tableNr - 1].getEntries())
                {
                    Console.WriteLine("Key: " + e.Key + ", Value: " + e.Value);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        //creates a new table and adds it to the table list
        public void createTable( String name, int colNr, Dictionary<String,String> cols)
        {   
            try
            {
                Table table = new Table(name, colNr, cols);
                //  XmlDocument xmlDoc = new XmlDocument();
                //  xmlDoc.Load(metadata);

                tableList.Add(table);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        //deletes a table from the list
        public void deleteTable(int index)
        {
            try
            {
                tableList.RemoveAt(index - 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

//----------------------------------------------------------
        public void addEntryinTable()
        {
            try { }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        public void removeEntryFromTable()
        {
            try { }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        public void updateEntryFromTable()
        {
            try { }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

    }
    
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace sqlLanguageImplementation
{
    class Controller
    {
        
        String metadata;
        String tableEntries;
        List<Table> tableList;
        int stringMaxLength = 25;


        //======constructors=====

        public Controller(String metadata,String tables)
        {
            this.metadata = metadata;
            this.tableEntries = tables;
            tableList = new List<Table>();
            

            // reads the metadata file
            if (File.Exists(metadata))
            {
                XDocument xmlDoc = XDocument.Load(metadata);
                var tabs = from el in xmlDoc.Descendants().Elements("Table") select el;
                foreach (var t in tabs)
                {
                    int pkflag = 1;
                    int i = t.Descendants().Count();
                    string name = (string)t.Descendants().ElementAt(0);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int j = 2; j <= i; j++)
                    {
                        dic.Add((string)t.Descendants().ElementAt(j - 1), (string)t.Descendants().ElementAt(j - 1).FirstAttribute );
                        if((t.Descendants().ElementAt(j - 1).Attribute("Key") != null)&&((string)t.Descendants().ElementAt(j - 1).Attribute("Key")== "Primary_Key")) 
                            {
                            pkflag = j - 1;
                            }
                    }

                    Table tab = new Table(name, i - 1,pkflag, dic);
                    tableList.Add(tab);
                }
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<Tables></Tables>");
                xmlDoc.Save(metadata);
            }


            // reads the table entries file
            readTableEntryesFromXml(tables);
           
        }

        public Controller( List<Table> tables, String metadata, String tablesEntries)
        {
            this.metadata = metadata;
            this.tableEntries = tablesEntries;
            this.tableList = tables;
        }

       
        //=======methods========
        //prints only the table names

        public void readTableEntryesFromXml( string tables)
        {
            if (File.Exists(tables))
            {
                string name;
                XDocument xmlDoc = XDocument.Load(tables);
                var tabs = from el in xmlDoc.Descendants().Elements() select el;
                foreach (var t in tabs)
                {
                    int i = t.Descendants().Count();
                    name = t.Name.ToString();
                    Dictionary<int, string> dic = new Dictionary<int, string>();
                    for (int j = 0; j < i; j++)
                    {
                        dic.Add((int)t.Descendants().ElementAt(j).FirstAttribute, (string)t.Descendants().ElementAt(j));
                    }

                    for (int k = 0; k < tableList.Count; k++)
                    {
                        if (tableList[k].name == name)
                        {
                            tableList[k].tableEntryList = dic;
                        }
                    }
                }

            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<Tables></Tables>");
                xmlDoc.Save(tables);
            }
        }
        public void showTableList()
        {
            
            try
            {               
                Console.WriteLine("");
                int i = 0;
                foreach (var t in tableList)
                {
                    i++;
                    Console.WriteLine(i+". "+t.name);
                }
                Console.WriteLine("");
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
                Console.WriteLine("");
                int i = 0;
                foreach (var t in tableList)
                {
                    i++;
                    Console.WriteLine(i+". "+t.toString());
                }
                Console.WriteLine("");
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
                readTableEntryesFromXml(tableEntries);
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
        public void createTable( String name, int colNr,int pkFlag, Dictionary<String,String> cols)
        {   
            try
            {
                Table table = new Table(name, colNr, pkFlag, cols);
                

                if (File.Exists(metadata))
                {
                    XDocument xmlDoc = XDocument.Load(metadata);
                    XElement root = new XElement("Table");
                    root.Add(new XElement("Name", name));
                    int index = 0;
                    foreach (var e in cols)
                    {
                        index++;
                        
                        XElement x = new XElement("Column", e.Key);
                        x.SetAttributeValue("Type",e.Value);
                        if(index == pkFlag)
                        {
                            x.SetAttributeValue("Key", "Primary_Key");
                        }
                        root.Add(x);
                        
                    }

                    xmlDoc.Element("Tables").Add(root);
                    xmlDoc.Save(metadata);
                   

                }
                else
                {
                    // Create the XmlDocument.
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml("<Tables></Tables>");
                    xmlDoc.Save(metadata);

                    XDocument doc = XDocument.Load(metadata);
                    XElement root = new XElement("Table");
                    root.Add(new XElement("Name", name));
                    foreach (var e in cols)
                    {
                        XElement x = new XElement("Column", e.Key);
                        x.SetAttributeValue("Type", e.Value);
                        root.Add(x);

                    }

                    doc.Element("Tables").Add(root);
                    doc.Save(metadata);
                }



                // inserts in the tables xml the table name
                XDocument xmlDoc1 = XDocument.Load(tableEntries);
                XElement root1 = new XElement(name);
                xmlDoc1.Element("Tables").Add(root1);
                xmlDoc1.Save(tableEntries);

                tableList.Add(table);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message +"create table");
            };
        }

        //deletes a table from the list
        public void deleteTable(int index)
        {
            try
            {
                XDocument doc = XDocument.Load(metadata);

                string tableName = tableList[index - 1].name;
                doc.Descendants("Table")
                     .Where(x => x.Element("Name").Value == tableName)
                     .Remove();
                doc.Save(metadata);

                tableList.RemoveAt(index - 1);

                XDocument doc1 = XDocument.Load(tableEntries);
                doc1.Descendants().Elements(tableName).Remove();
                doc1.Save(tableEntries);

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

//----------------------------------------------------------
        public void addEntryinTable(int index)
        {
            //try {
            //    Console.WriteLine(" Insert the data separated by a space");
            //    foreach(var a in tableList[index - 1].columnsNameTypeList)
            //    {
            //        Console.Write(" "+a.Value);
            //    }
            //    Console.Write("\n");
            //    string name = tableList[index - 1].name;

            //    string s = Console.ReadLine();
            //    //string[] words = s.Split(' '); 
            //    tableList[index - 1].addEntry(s);

            string input = "";
            string finalString = "";
            try
            {
                Console.WriteLine("Insert data for the columns, the max size of each value can be 25");
                foreach (var a in tableList[index - 1].columnsNameTypeList)
                {
                    Console.WriteLine("Introduce value for the column: " + a.Key);
                    input = "";
                    input = Console.ReadLine();

                    if (input.Length > stringMaxLength)
                    {
                        input = input.Substring(0, stringMaxLength); //verific daca e mai mare de 25 si il tau
                        //throw new IOException("Value is too long"); //daca vrem sa nu se adauge valoare si sa folosim o exceptie
                    }
                    else if (input.Length < stringMaxLength)
                    {
                        input = input.PadRight(stringMaxLength); // verific daca e mai mic de 25 si adaug white space pana la length 25
                    }

                    finalString += input;// face concat la coloana la stringul mare
                    finalString += " ";
                }

                Console.Write("\n");
                finalString.Trim();

                string name = tableList[index - 1].name;
                tableList[index - 1].addEntry(finalString);
                int key = tableList[index - 1].entryNr;
                XDocument xmlDoc1 = XDocument.Load(tableEntries);
                var element = xmlDoc1.Element("Tables")
                    .Elements(name).First();

                XElement x = new XElement("Entry", finalString);
                x.SetAttributeValue("Key", key);
                element.Add(x);
                xmlDoc1.Save(tableEntries);



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message +" add entry");
            };
        }

        public void removeEntryFromTable(int index)
        {
            try
            {
                showTableContent(index);
                Console.WriteLine("\n Enter the Key to delete the entry");
                string keyIndex= (Console.ReadLine());
                string name = tableList[index - 1].name;

                XDocument xmlDoc = XDocument.Load(tableEntries);
                var element = xmlDoc.Element("Tables")
                    .Elements(name).First();
                element.Descendants().Where(x => (string)x.Attribute("Key") == keyIndex).Remove();

                xmlDoc.Save(tableEntries);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        public void updateEntryFromTable(int index)
        {
            try
            {
                showTableContent(index);
                Console.WriteLine("\n Enter the Key to update the entry");
                string keyIndex = (Console.ReadLine());
                string name = tableList[index - 1].name;

                //Console.WriteLine("\n Enter the new value");
                //string value = Console.ReadLine();
                //XDocument xmlDoc = XDocument.Load(tableEntries);
                //var element = xmlDoc.Element("Tables")
                //    .Elements(name).First();
                //element.Descendants().Where(x => (string)x.Attribute("Key") == keyIndex).Single().SetValue(value);
                //xmlDoc.Save(tableEntries);


                string input = "";
                string updatedString = "";
                int nbOfColumns = tableList[index - 1].columnNr;
                int count = 0;
                string currentEntry = "";

                //iau valoare currenta a entryului ca sa pot face split pe el

                Dictionary<int, string> vals = tableList[index - 1].getEntries();
                string[] colValues;
                currentEntry = vals[Int32.Parse(keyIndex)];
                currentEntry = currentEntry.Trim();

                colValues = currentEntry.Split(' ');
                colValues = colValues.Where(w => w != "").ToArray();


                foreach (var a in tableList[index - 1].columnsNameTypeList)
                {
                    Console.WriteLine("Introduce value for the column: " + a.Key);
                    input = "";
                    input = Console.ReadLine();
                    string savedValue = colValues[count];

                    if (input.Length == 0)
                    {

                        if (savedValue.Length < 25)
                        {
                            savedValue = savedValue.PadRight(stringMaxLength);// daca e prea scurt ii dau maxLength
                        }
                        updatedString += savedValue;
                        updatedString += " ";
                    }
                    else if (input.Length > stringMaxLength)
                    {
                        input = input.Substring(0, stringMaxLength); //verific daca e mai mare de 25 si il tau
                        updatedString += input;
                        updatedString += " ";
                        //throw new IOException("Value is too long"); //daca vrem sa nu se adauge valoare si sa folosim o exceptie
                    }
                    else if (input.Length < stringMaxLength)
                    {
                        input = input.PadRight(stringMaxLength); // verific daca e mai mic de 25 si adaug white space pana la length 25
                        updatedString += input;
                        updatedString += " ";
                    }
                    count++;
                }



                XDocument xmlDoc = XDocument.Load(tableEntries);
                var element = xmlDoc.Element("Tables")
                    .Elements(name).First();
                element.Descendants().Where(x => (string)x.Attribute("Key") == keyIndex).Single().SetValue(updatedString);


                xmlDoc.Save(tableEntries);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

    }
    
}

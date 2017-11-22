using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlLanguageImplementation
{
    class Ui
    {
        Controller controller = new Controller("metadata.xml","tables.xml");

        public Ui()
        {

        }

       

        public void showUI()
        {
            Console.WriteLine(
                "1. Show Table List \n"+
                "2. Show Detailed Table List \n"+
                "3. Add a Table \n"+
                "4. Delete a Table \n" +
                "5. Print Table entries \n"+
                "6. Add an entry ia a Table \n"               
                );

            string s = Console.ReadLine();
            int opt= -1;
            int.TryParse(s,out opt);
            switch (opt)
            {
                case 1:
                    controller.showTableList();
                    break;
                case 2:
                    controller.showTableListDetailed();
                    break;
                case 3:
                    readNewTable();
                    break;
                case 4:
                    Console.WriteLine("Insert table Index to be deleted");
                    int i;
                    int.TryParse(Console.ReadLine(), out i);
                    controller.deleteTable(i);
                    break;
                case 5:
                    Console.WriteLine("Insert table Index to be printed");
                    int j;
                    int.TryParse(Console.ReadLine(), out j);
                    controller.showTableContent(j);
                    break;
                case 6:
                    Console.WriteLine("Insert table Index to add an Entry");
                    int k;
                    int.TryParse(Console.ReadLine(), out k);
                    controller.addEntryinTable(k);
                    break;

                default:
                    return;
            }
            showUI();
        }




        public void readNewTable()
        {

            Console.WriteLine("Insert Table name: ");
            string name = Console.ReadLine();


            Console.WriteLine("Insert Columns number:");
            int colNr;
            int.TryParse(Console.ReadLine(), out colNr);


            Console.WriteLine("Please insert the Columns and their Type separated by a space");
            Dictionary<String, String> columnTypes = new Dictionary<string, string>();
            for (int i = 0; i < colNr; i++)
            {
                Console.WriteLine("Column {0}: ", i);
                string s = Console.ReadLine();
                string[] words = s.Split(' ');
                if (words.Count() != 2)
                {
                    Console.WriteLine(" Invalid input at column or type !");
                    return;
                }
                columnTypes.Add(words[0], words[1]);


            }
            controller.createTable(name, colNr, columnTypes);
        }

    }


}

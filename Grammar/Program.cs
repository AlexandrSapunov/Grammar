using Grammar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            GrammarDeterminer determiner = new GrammarDeterminer();

            determiner.AddGrammar("S","A|B|C");
            determiner.AddGrammar("A","Ab|b");
            determiner.AddGrammar("B","Bc|a");
            determiner.AddGrammar("C","c|Ba");

            determiner.ShowGrammar();
            determiner.Translate("S");

            Console.WriteLine();
            determiner.ReverseTranslation("aca");
            Console.ReadKey();
        }
    }
}

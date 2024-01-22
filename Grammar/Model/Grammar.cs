using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammar.Model
{
    public class Grammar
    {
        public string Nonterminal { get; set; }
        public List<string> Terminal { get; set; }

        public Grammar(string nterminal,string [] terminal) 
        {
            Nonterminal= nterminal;
            Terminal = new List<string>(terminal);
        }
    }
}

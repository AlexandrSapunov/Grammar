using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grammar.Model
{
    public class GrammarDeterminer
    {
        private List<Grammar> _grammars { get; set; }

        public GrammarDeterminer() 
        {
            _grammars = new List<Grammar>();
        }

        public void AddGrammar(string nonterminal, string terminal)
        {
            Grammar grammar = new Grammar(nonterminal,terminal.Split('|'));
            _grammars.Add(grammar);
        }

        public void ShowGrammar()
        {
            Console.WriteLine("Grammar:");
            foreach (Grammar grammar in _grammars) 
            {
                Console.Write($"{grammar.Nonterminal}->");
                for(int i = 0; i < grammar.Terminal.Count;i++)
                {
                    Console.Write($"{grammar.Terminal[i]}");
                    if (i != grammar.Terminal.Count - 1)
                        Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        /*
         * S->A|B|C
         * A->a|b
         * B->a|c
         * C->c|b
         */
        public void Translate(string nonterminal)
        {
            Console.Write($"{nonterminal}");
            Random rnd = new Random();
            while (nonterminal.Any(char.IsUpper))
            {
                foreach (Grammar grammar in _grammars)
                {
                    if (nonterminal.Contains(grammar.Nonterminal))
                    {
                        Console.Write("->");
                        nonterminal = nonterminal.Replace(grammar.Nonterminal, grammar.Terminal[rnd.Next(0, grammar.Terminal.Count)]);
                        Console.Write($"{nonterminal}");
                    }
                }
            }
        }

        public void ReverseTranslation(string terminal)
        {
            List<(string term, string nonterm)> tempItems = new List<(string term, string nonterm)>();
            
            List<Grammar> baseGrammars = new List<Grammar>(_grammars);
            foreach (Grammar grammar in baseGrammars)
            {
                if (grammar.Nonterminal == "S")
                {
                    baseGrammars.Remove(grammar);
                    break;
                }
            }

            for(int i = 0; i < baseGrammars.Count; i++)
            {
                for (int j = 0; j < baseGrammars[i].Terminal.Count; j++)
                {
                    tempItems.Add((baseGrammars[i].Terminal[j].ToString(), baseGrammars[i].Nonterminal.ToString()));
                }
            }
            tempItems.Sort((x, y) => y.term.Length.CompareTo(x.term.Length));
            foreach(var item in tempItems)
            {
                Console.WriteLine($"{item.term}->{item.nonterm}");
            }

            Console.WriteLine();
            Console.Write(terminal);
            int isBreak = 0;

            while (terminal.Any(char.IsLower))
            {
                for(int i = 0; i < tempItems.Count; i++)
                {
                    if (tempItems[i].term.Length > 1)
                    {
                        string twoSymbol = string.Join("",new char[] { terminal[0], terminal[1] });
                        if (tempItems[i].term == twoSymbol)
                        {
                            terminal = tempItems[i].nonterm + terminal.Substring(2);
                            Console.Write($"->{terminal}");
                            isBreak = 0;
                            break;
                        }
                        else
                            isBreak++;
                    }
                    else if (tempItems[i].term.Length == 1)
                    {
                        if (tempItems[i].term == terminal[0].ToString())
                        {
                            terminal = tempItems[i].nonterm + terminal.Substring(1);
                            Console.Write($"->{terminal}");
                            isBreak = 0;
                            break;
                        }
                        else
                            isBreak++;
                    }
                }
                if (terminal.Length== 1)
                {
                    List<Grammar> lastTranslationItems = _grammars.Where(x => x.Nonterminal == "S").ToList();
                    for(int i = 0; i < lastTranslationItems.Count; i++)
                    {
                        for(int j = 0; j<lastTranslationItems[i].Terminal.Count(); j++)
                        {
                            if (lastTranslationItems[i].Terminal[j] == terminal)
                            {
                                Console.Write($"->{lastTranslationItems[i].Nonterminal}");
                                Console.WriteLine("\nПринадлежит к текущей грамматике");
                                return;
                            }
                        }
                    }
                }
                if (isBreak >= tempItems.Count())
                {
                    Console.WriteLine();
                    Console.WriteLine("Не принадлежит текущей грамматике!");
                    break;
                }
            }

        }

    }
}

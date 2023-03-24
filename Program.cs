﻿internal class Program
{
    #region MENU
    static char Menu()
    {
        char m;

        Console.Clear();
        Console.WriteLine(">>>> Menu Principal <<<<");
        Console.WriteLine("[C]riar Orçamento");
        Console.WriteLine("[L]istar Orçamentos");
        Console.WriteLine("[A]provar Orçamento");
        Console.WriteLine("[S]air Orçamento");

        try
        {
            m = char.Parse(Console.ReadLine().ToLower());
        }

        catch
        {
            return '\n';
        }

        return m;
    }
    #endregion
    private static void Main(string[] args)
    {
        string path = "quotes.dat";
        List<Quotes> quotations = new();
        LoadFromFile(path, quotations);

        #region SWITCHASE
        do
        {
            switch (Menu())
            {
                case 'c':
                    quotations = CreateQuotations(quotations);
                    break;
                case 'l':
                    ListQuotations(quotations);
                    break;
                case 'a':
                    QuotationsForApproval();
                    break;
                case 's':
                    DumpToFile(quotations, path);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção Inválida!");
                    Console.Beep();
                    Thread.Sleep(1000);
                    break;
            }
        } while (true);
        #endregion
    }

    private static List<Quotes> LoadFromFile(string p, List<Quotes> l)
    {
        
        if (File.Exists(p))
        {
            StreamReader sr = new(p);
            do
            {
                string[] quote = sr.ReadLine().Split(",");
                string id = quote[0];
                string d = quote[1];
                double v = double.Parse(quote[2]);

                l.Add(new(id, d, v));
            } while (!sr.EndOfStream);
            sr.Close();
            sr.Close();
        }
        else
            Console.WriteLine("Arquivo não encontrado!");

        return l;
    }

    private static void DumpToFile(List<Quotes> l, string p)
    {
        StreamWriter sw = new(p);
        try
        {
            foreach (var item in l)
            {
                sw.WriteLine(item.SaveToFile());
            }
        }
        catch (Exception e)
        {
            p = p + "error.log";
            sw = new(p);

            sw.WriteLine(e.Message.ToString());
        }

        sw.Close();
    }

    private static void QuotationsForApproval()
    {
        throw new NotImplementedException();
    }

    private static void ListQuotations(List<Quotes> l)
    {
        foreach (var quote in l)
        {
            Console.Clear();
            Console.WriteLine(quote.ToString());
            Console.Write("Pressione qualquer tecla para o próximo da lista...");
            Console.ReadLine();
        }
        Console.ReadLine();
    }

    private static List<Quotes> CreateQuotations(List<Quotes> l)
    {
        Console.WriteLine("Informe a Descrição da Cotação: ");
        string d = Console.ReadLine();
        Console.WriteLine("Informe o Valor da Cotação: ");
        double v = double.Parse(Console.ReadLine());
        Console.WriteLine("");

        Quotes q = new(d, v);
        l.Add(new(d, v));

        return l;
    }
}
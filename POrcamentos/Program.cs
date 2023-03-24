internal class Program
{
    static char Menu()
    {
        char m;

        Console.Clear();
        Console.WriteLine(">>>> Menu Principal <<<<");
        Console.WriteLine("[C]riar Orçamento");
        Console.WriteLine("[L]istar Orçamentos");
        Console.WriteLine("[A]provar Orçamento");
        Console.WriteLine("L[i]star Orçamentos Aprovados");
        Console.WriteLine("[S]air do Programa");

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

    private static void Main(string[] args)
    {
        string quotesfile = "quotes.dat";
        string approvalsfile = "approvals.dat"; 

        List<Quotes> quotations = new();
        List<Quotes> approvals = new();

        quotations = LoadFromFile(quotesfile, quotations);
        approvals = LoadFromFile(approvalsfile, approvals);


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
                    approvals = QuotationsForApproval(quotations);
                    break;

                case 'i':
                    ListQuotations(approvals);
                    break;

                case 's':
                    DumpToFile(quotations, quotesfile);
                    DumpToFile(approvals, approvalsfile);
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção Inválida!");
                    Console.Beep();
                    Thread.Sleep(1000);
                    break;
            }
        } while (true);
        
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
            Console.WriteLine("Arquivo sendo criado...");

        Thread.Sleep(2000);
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
            p = "error.log";
            sw = new(p);

            sw.WriteLine(e.Message.ToString());
        }

        sw.Close();
    }

    private static List<Quotes> QuotationsForApproval(List<Quotes> quotations)
    {
        List<Quotes> a = new();

        if (quotations.Count == 0)
        {
            Console.WriteLine("Nenhum item na lista...");
            Thread.Sleep(2000);
        }
        else
        {
            foreach (var item in quotations)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("Deseja aprovar esta cotação? (S/N)");
                char c = char.Parse(Console.ReadLine().ToLower());
                if (c == 's')
                {
                    a.Add(item);
                }
            }


        }
        return a;
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
        //Console.ReadLine();
    }

    private static List<Quotes> CreateQuotations(List<Quotes> l)
    {
        Console.WriteLine("Informe a Descrição da Cotação: ");
        string d = Console.ReadLine();
        Console.WriteLine("Informe o Valor da Cotação: ");
        double v = double.Parse(Console.ReadLine());
        
        l.Add(new(d, v));

        return l;
    }
}
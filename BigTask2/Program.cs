using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using BigTask2.RequestHandler;
namespace BigTask2
{
	class Program
	{
        static IEnumerable<Route> ServeRequest(Request request)
        {
            (IGraphDatabase cars, IGraphDatabase trains) = MockData.InitDatabases();
            /*
			 *
			 * Add request handling here and return calculated route
			 *
			 */
            IRequest vert = new DataVerification();
            IRequest prob = new PickProblem();
            IRequest merg = new MergingDatase(cars, trains);
            IRequest filt = new AddFilter();
            IRequest solv = new PickSolver();

            vert.SetNext(prob);
            prob.SetNext(merg);
            merg.SetNext(filt);
            filt.SetNext(solv);

            return vert.Handle(request, null); 
		}
		static void Main(string[] args)
		{
            uiFactory factory;         
            Console.WriteLine("---- Xml Interface ----");
            /*
			 * Create XML System Here
             * and execute prepared strings
			 */
            factory = new XmlFactory();
            Execute(CreateSystem(factory), "xml_input.txt");
            Console.WriteLine();

            Console.WriteLine("---- KeyValue Interface ----");
            /*
			 * Create INI System Here
             * and execute prepared strings
			 */
            factory = new KvFactory();
            Execute(CreateSystem(factory), "key_value_input.txt");
            Console.WriteLine();

            Console.ReadKey();
        }

        /* Prepare method Create System here (add return, arguments and body)*/
        static ISystem CreateSystem(uiFactory factory)
        {
            return factory.GetSystem();
        }

    static void Execute(ISystem system, string path)
        {
            IEnumerable<IEnumerable<string>> allInputs = ReadInputs(path);
            foreach (var inputs in allInputs)
            {
                foreach (string input in inputs)
                {
                    system.Form.Insert(input);
                }
                var request = RequestMapper.Map(system.Form);
                var result = ServeRequest(request);
                system.Display.Print(result);
                Console.WriteLine("==============================================================");
            }
        }

        private static IEnumerable<IEnumerable<string>> ReadInputs(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                List<List<string>> allInputs = new List<List<string>>();
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    List<string> inputs = new List<string>();
                    while (!string.IsNullOrEmpty(line))
                    {
                        inputs.Add(line);
                        line = file.ReadLine();
                    }
                    if (inputs.Count > 0)
                    {
                        allInputs.Add(inputs);
                    }
                }
                return allInputs;
            }
        }
    }
}

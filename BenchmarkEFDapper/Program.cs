//var customerService = new CustomerService();

//BenchmarkRunner.Run<Benchmarks>();

using BenchmarkEFDapper;

var customerService = new CustomerService();
Console.WriteLine("customer service started");
//customerService.Print();

customerService.InsertEF();

//customerService.UpdateAllEF();


//customerService.DeleteAllEF();





//Console.WriteLine("Hello, World!");
using BenchmarkDotNet.Running;
using BenchmarkEFDapper;

BenchmarkRunner.Run<Benchmarks>();

//var customerService = new CustomerService();

//customerService.InsertEF(10000);
//customerService.DeleteAllEF();

//customerService.InsertDapperContrib(10);
//customerService.DeleteDapperContrib();

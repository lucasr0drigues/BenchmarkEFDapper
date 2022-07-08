using BenchmarkDotNet.Running;
using BenchmarkEFDapper;

BenchmarkRunner.Run<Benchmarks>();

//var customerService = new CustomerService();

//customerService.InsertEF(10);
//customerService.UpdateAllEF();
//customerService.DeleteAllEF();

//customerService.InsertDapperContrib(10);
//customerService.UpdateDapperContrib();
//customerService.DeleteDapperContrib();

//customerService.InsertEFOneByOne(10);
//customerService.InsertDapperContribOneByOne(10);



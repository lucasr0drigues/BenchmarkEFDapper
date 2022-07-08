using BenchmarkDotNet.Attributes;

namespace BenchmarkEFDapper
{
    [MemoryDiagnoser]
    [RankColumn]
    public class Benchmarks
    {

        [Params(1, 20, 100)]
        public int Regs { get; set; }

        private readonly CustomerService _customerService = new();

        [Benchmark]
        public void EF()
        {
            _customerService.InsertEF(Regs);
            _customerService.UpdateAllEF();
            _customerService.DeleteAllEF();
        }

        [Benchmark]
        public void DapperContrib()
        {
            _customerService.InsertDapperContrib(Regs);
            _customerService.UpdateDapperContrib();
            _customerService.DeleteDapperContrib();
        }

        [Benchmark]
        public void EFOneByOne()
        {
            _customerService.InsertEFOneByOne(Regs);
            _customerService.UpdateAllEFOneByOne();
            _customerService.DeleteAllEF();
        }

        [Benchmark]
        public void DapperContribOneByOne()
        {
            _customerService.InsertDapperContribOneByOne(Regs);
            _customerService.UpdateDapperContribOneByOne();
            _customerService.DeleteDapperContrib();
        }



    }
}

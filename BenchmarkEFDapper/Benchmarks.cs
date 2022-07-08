using BenchmarkDotNet.Attributes;

namespace BenchmarkEFDapper
{
    [MemoryDiagnoser]
    [RankColumn]
    public class Benchmarks
    {

        [Params(1, 20, 500)]
        public int Regs { get; set; }

        private readonly CustomerService _customerService = new();

        [Benchmark]
        public void EF()
        {
            _customerService.InsertEF(Regs);
            _customerService.DeleteAllEF();
        }

        [Benchmark]
        public void DapperContrib()
        {
            _customerService.InsertDapperContrib(Regs);
            _customerService.DeleteDapperContrib();
        }
    }
}

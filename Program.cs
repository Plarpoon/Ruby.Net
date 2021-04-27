using System.Threading.Tasks;

namespace RubyNet
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await RubyBot.RunAsync(args);
    }
}
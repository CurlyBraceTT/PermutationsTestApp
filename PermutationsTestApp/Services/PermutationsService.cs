using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PermutationsTestApp.Model;

namespace PermutationsTestApp.Services
{
    public interface IPermutationsService
    {
        Task<List<Element>> GetAllAsync();
        Task<Element> CalculateAsync(string value);
        Task<Element> GetAsync(string value);
        Task AddAsync(Element element);
    }

    public class PermutationsService : IPermutationsService
    {
        private readonly PermutationsContext _context;

        public PermutationsService(PermutationsContext context)
        {
            _context = context;
        }

        public async Task<List<Element>> GetAllAsync()
        {
            return await _context.Elements.ToListAsync();
        }

        public async Task<Element> GetAsync(string value)
        {
            return await _context.Elements.SingleOrDefaultAsync(e => e.Value == value);
        }

        public async Task AddAsync(Element element)
        {
            await _context.Elements.AddAsync(element);
            await _context.SaveChangesAsync();
        }

        public async Task<Element> CalculateAsync(string value)
        {
            var element = await GetAsync(value);

            if(element == null)
            {
                var watch = Stopwatch.StartNew();
                var repetitions = CountDuplicates(value);

                var count = 1;
                for (var i = repetitions + 1; i <= value.Length; i++)
                {
                    count *= i;
                }

                watch.Stop();

                element = new Element
                {
                    Value = value,
                    CalculatedTime = watch.ElapsedTicks,
                    PermutationCount = count
                };
                await AddAsync(element);
            };

            return element;
        }

        private int CountDuplicates(string value)
        {
            var duplicates = new Dictionary<char, int>();

            foreach (var c in value)
            {
                if (!duplicates.ContainsKey(c))
                {
                    duplicates.Add(c, 1);
                }
                else
                {
                    duplicates[c]++;
                }
            }

            return duplicates.Where(d => d.Value > 1).Sum(d => d.Value);
        }
    }
}

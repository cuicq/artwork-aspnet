
namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 种子编号生成器。
    /// </summary>
    public sealed class SeedCodeGenerator : ICodeGenerator
    {
        private readonly ISeedStore _seedStore;
        private readonly string _seedKey;
        private readonly int _seedWidth;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public SeedCodeGenerator(ISeedStore seedStore, string seedKey, int seedWidth)
        {
            _seedStore = seedStore;
            _seedKey = seedKey;
            _seedWidth = seedWidth;
        }

        /// <inheritdoc />
        public string Generate(GenerateContext context)
        {
            var seed = _seedStore.NextSeed(_seedKey);

            return seed.ToString().PadLeft(_seedWidth, '0');
        }
    }
}


namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 字母种子编号生成器。
    /// </summary>
    public sealed class LetterSeedCodeGenerator : ICodeGenerator
    {
        private readonly ISeedStore _seedStore;
        private readonly string _seedKey;
        private readonly int _seedWidth;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public LetterSeedCodeGenerator(ISeedStore seedStore, string seedKey, int seedWidth)
        {
            _seedStore = seedStore;
            _seedKey = seedKey;
            _seedWidth = seedWidth;
        }

        /// <inheritdoc />
        public string Generate(GenerateContext context)
        {
            var seed = _seedStore.NextSeed(_seedKey);

            return this.ToLetter(seed).PadLeft(_seedWidth, 'A');
        }

        private string ToLetter(int seed)
        {
            var letter = "";
            do
            {
                var quotient = seed % 26;
                seed = seed / 26;
                letter = (char)(quotient + 65) + letter;
            } while (seed != 0);

            return letter;
        }
    }
}

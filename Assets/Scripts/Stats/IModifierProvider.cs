using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        /// <summary>
        /// The modifier is positive if it increases base stat and negative if it decreases it.
        /// </summary>
        /// <param name="stat">Which stat to apply it to.</param>
        /// <returns>A enumerable list of floats with all additive modifieres in the implementing class.</returns>
        public IEnumerable<float> GetAdditiveModifiers(Stats stat);
        /// <summary>
        /// This should use factorised percentage, meaning 1.00 = 0%, 0.80 = -20% and 1.2 = +20%.
        /// </summary>
        /// <param name="stat">Which stat to apply it to.</param>
        /// <returns>A enumerable list of floats with all multiplying modifieres in the implementing class.</returns>
        public IEnumerable<float> GetMultiplyingModifiers(Stats stat);
    }
}

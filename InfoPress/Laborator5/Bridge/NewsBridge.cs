namespace InfoPress.Bridge
{
    // Implementation Interface
    public interface IFormatAfisare
    {
        void FormatText(string text);
    }

    // Concrete Implementation A: Web
    public class FormatWeb : IFormatAfisare
    {
        public void FormatText(string text) => System.Console.WriteLine($"<html><body>{text}</body></html>");
    }

    // Concrete Implementation B: Json/API
    public class FormatJson : IFormatAfisare
    {
        public void FormatText(string text) => System.Console.WriteLine($"{{\"content\": \"{text}\"}}");
    }

    // Abstraction
    public abstract class MesajStire
    {
        protected IFormatAfisare _format;
        public MesajStire(IFormatAfisare format) => _format = format;
        public abstract void Trimite(string continut);
    }

    // Refined Abstraction
    public class StireUrgenta : MesajStire
    {
        public StireUrgenta(IFormatAfisare format) : base(format) { }
        public override void Trimite(string continut)
        {
            _format.FormatText($"URGENT: {continut}");
        }
    }
}

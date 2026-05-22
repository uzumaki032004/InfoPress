using InfoPress.Interfaces;
using System;

namespace InfoPress.Decorator
{
    // Base Decorator
    public abstract class ArticolDecorator : IArticol
    {
        protected IArticol _articol;

        public ArticolDecorator(IArticol articol)
        {
            _articol = articol;
        }

        public string Title { get => _articol.Title; set => _articol.Title = value; }
        public string ContentText { get => _articol.ContentText; set => _articol.ContentText = value; }
        public string Author { get => _articol.Author; set => _articol.Author = value; }
        public string Category { get => _articol.Category; set => _articol.Category = value; }
        public DateTime CreatedDate { get => _articol.CreatedDate; set => _articol.CreatedDate = value; }
        public bool IsPremium { get => _articol.IsPremium; set => _articol.IsPremium = value; }
        public int ViewCount { get => _articol.ViewCount; set => _articol.ViewCount = value; }

        public IArticol WrappedArticle => _articol;

        public virtual void AfiseazaArticol()
        {
            _articol.AfiseazaArticol();
        }
    }

    // Premium Decorator
    public class PremiumArticolDecorator : ArticolDecorator
    {
        public PremiumArticolDecorator(IArticol articol) : base(articol) 
        {
            this.IsPremium = true;
        }

        public override void AfiseazaArticol()
        {
            Console.Write("[ACCES EXCLUSIV] ");
            base.AfiseazaArticol();
        }
    }
}

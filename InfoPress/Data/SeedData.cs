using InfoPress.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InfoPress.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // 1. Create Roles
                string[] roles = { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // 2. Create Admin User
                string adminEmail = "admin@infopress.ro";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        DisplayName = "Administrator",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(adminUser, "Admin123");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // 3. Seed Articles with UNIQUE and RELEVANT IDs
                if (!context.Articles.Any())
                {
                    var articles = new List<NewsArticle>
                    {
                        new NewsArticle { Title = "Revoluție în Tehnologie", ContentText = "O nouă descoperire promite să schimbe modul în care folosim energia solară...", Category = "Tehnologie", Summary = "Descoperire majoră în domeniul energiei regenerabile.", ImageUrl = "https://images.unsplash.com/photo-1518770660439-4636190af475?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Economia în 2026", ContentText = "Analiștii prevăd o creștere stabilă pentru piețele emergente...", Category = "Economie", Summary = "Perspective economice optimiste pentru anul curent.", ImageUrl = "https://images.unsplash.com/photo-1591696205602-2f950c417cb9?w=1200&q=80", IsPremium = true, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Meci decisiv în Liga Campionilor", ContentText = "Echipele se pregătesc pentru confruntarea finală de pe stadionul Wembley...", Category = "Sport", Summary = "Finala mult așteptată are loc sâmbăta aceasta.", ImageUrl = "https://images.unsplash.com/photo-1461896836934-ffe607ba8211?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Noi legi în Parlament", ContentText = "Parlamentarii au votat astăzi pachetul de legi privind educația...", Category = "Politica", Summary = "Schimbări importante în sistemul de învățământ.", ImageUrl = "https://images.unsplash.com/photo-1529107386315-e1a2ed48a620?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Editorial: Viitorul Presei", ContentText = "Cum influențează inteligența artificială jurnalismul modern...", Category = "Editorial", Summary = "O analiză despre evoluția mijloacelor de informare.", ImageUrl = "https://images.unsplash.com/photo-1504711434969-e33886168f5c?w=1200&q=80", IsPremium = true, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Gadgeturi de ultimă oră", ContentText = "Cele mai noi telefoane lansate la târgul de tehnologie...", Category = "Tehnologie", Summary = "Top gadgeturi pe care trebuie să le ai.", ImageUrl = "https://images.unsplash.com/photo-1519389950473-47ba0277781c?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Piața imobiliară în scădere", ContentText = "Prețurile apartamentelor au început să scadă în marile orașe...", Category = "Economie", Summary = "Oportunități noi pentru cumpărători.", ImageUrl = "https://images.unsplash.com/photo-1560518883-ce09059eeffa?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Transfer bombă în fotbal", ContentText = "Unul dintre cei mai buni jucători ai lumii pleacă la o rivală...", Category = "Sport", Summary = "Mutare neașteptată pe piața transferurilor.", ImageUrl = "https://images.unsplash.com/photo-1574629810360-7efbbe195018?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Alegeri anticipate?", ContentText = "Discuțiile despre organizarea alegerilor înainte de termen continuă...", Category = "Politica", Summary = "Tensiuni politice la nivel înalt.", ImageUrl = "https://images.unsplash.com/photo-1520690216127-2475e323e27e?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Cultură și Artă", ContentText = "O noua expoziție impresionantă s-a deschis la muzeul național...", Category = "Editorial", Summary = "Eveniment cultural de excepție.", ImageUrl = "https://images.unsplash.com/photo-1513364776144-60967b0f800f?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Descoperiri în medicină", ContentText = "Cercetătorii au găsit o noua metodă de tratament pentru...", Category = "Tehnologie", Summary = "Speranțe noi pentru pacienți.", ImageUrl = "https://images.unsplash.com/photo-1576086213369-97a306d36557?w=1200&q=80", IsPremium = true, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Investiții în Crypto", ContentText = "Bitcoin atinge un nou record istoric în această dimineață...", Category = "Economie", Summary = "Piața activelor digitale este în fierbere.", ImageUrl = "https://images.unsplash.com/photo-1518546305927-5a555bb7020d?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Tenis: Finala de la Roland Garros", ContentText = "O finală dramatică între doi titani ai sportului alb...", Category = "Sport", Summary = "Spectacol total pe zgura pariziană.", ImageUrl = "https://images.unsplash.com/photo-1626307334407-3444410507ed?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Relații Internaționale", ContentText = "Liderii mondiali s-au întâlnit pentru a discuta criza climatică...", Category = "Politica", Summary = "Acorduri importante semnate la summit.", ImageUrl = "https://images.unsplash.com/photo-1526628953301-3e589a6a8b74?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" },
                        new NewsArticle { Title = "Critica Literară", ContentText = "Recenzia celei mai vândute cărți a lunii mai...", Category = "Editorial", Summary = "O lectură obligatorie pentru pasionații de literatură.", ImageUrl = "https://images.unsplash.com/photo-1474366521946-c3d4b507abf2?w=1200&q=80", IsPremium = false, Author = "Redacția InfoPress" }
                    };
                    context.Articles.AddRange(articles);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

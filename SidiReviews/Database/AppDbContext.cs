using Microsoft.EntityFrameworkCore;
using SidiReviews.Commons;
using SidiReviews.Model;
using System;

namespace SidiReviews.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"DataSource={GlobalParameters.DbPath}");
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1, 
                    Title = "Matrix",
                    Genre = MovieGenre.ScienceFiction,
                    Director = "Wachowskis",
                    ReleaseDate = new DateTime(1999, 3, 31),
                    ImagePath = "ms-appx:///Assets/matrix.jpg",
                    Synopsis = "Ambientado no século 22, Matrix conta a história de um hacker de computador que se junta a um" +
                    "grupo de insurgentes subterrâneos lutando contra os poderosos computadores que agora governam a Terra."
                },
                new Movie
                {
                    Id = 2,
                    Title = "City of God",
                    Genre = MovieGenre.Crime,
                    Director = "Fernando Meirelles",
                    ReleaseDate = new DateTime(2002, 8, 30),
                    ImagePath = "ms-appx:///Assets/city-of-god.jpg",
                    Synopsis = "Nas favelas empobrecidas do Rio de Janeiro na década de 1970, dois jovens escolhem caminhos diferentes." +
                    "Buscapé é um fotógrafo iniciante que documenta a crescente violência relacionada às drogas em seu bairro," +
                    "enquanto José \"Zé\" Pequeno é um ambicioso traficante de drogas mergulhando em uma vida perigosa de crime."
                },
                new Movie
                {
                    Id = 3,
                    Title = "Inception",
                    Genre = MovieGenre.ScienceFiction, 
                    Director = "Christopher Nolan",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    ImagePath = "ms-appx:///Assets/inception.jpg",
                    Synopsis = "Cobb, um ladrão habilidoso que comete espionagem corporativa ao infiltrar-se no subconsciente de seus alvos," +
                    " é oferecido a chance de recuperar sua antiga vida como pagamento por uma tarefa considerada impossível: \"inception\"," +
                    " a implantação da ideia de outra pessoa no subconsciente de um alvo."
                },
                new Movie
                {
                    Id = 4,
                    Title = "A Minecraft Movie",
                    Genre = MovieGenre.Action,
                    Director = "Jared Hess",
                    ReleaseDate = new DateTime(2010, 4, 3),
                    ImagePath = "ms-appx:///Assets/minecraft.jpg",
                    Synopsis = "Quatro desajustados se encontram lutando com problemas comuns quando são repentinamente puxados" +
                    "através de um portal misterioso para o Overworld:" +
                    "um mundo bizarro e cúbico que prospera na imaginação." +
                    " Para voltar para casa, eles terão que dominar este mundo enquanto embarcam em uma jornada mágica com um artesão inesperado e especialista, Steve."
                },
                new Movie
                {
                    Id = 5,
                    Title = "Anora",
                    Genre = MovieGenre.Comedy,
                    Director = "Sean Baker",
                    ReleaseDate = new DateTime(2025, 1, 23),
                    ImagePath = "ms-appx:///Assets/anora.jpg",
                    Synopsis = "Anora, uma jovem trabalhadora sexual do Brooklyn," +
                    " tem sua chance de viver uma história de Cinderela quando conhece e impulsivamente se casa com o filho de um oligarca." +
                    " Quando a notícia chega à Rússia, seu conto de fadas é ameaçado, pois os pais partem para Nova York para anular o casamento."
                },
                new Movie
                {
                    Id = 6,
                    Title = "Sinners",
                    Genre = MovieGenre.Horror,
                    Director = "Ryan Coogler",
                    ReleaseDate = new DateTime(2025, 4, 18),
                    ImagePath = "ms-appx:///Assets/sinners.jpg",
                    Synopsis = "Tentando deixar suas vidas problemáticas para trás, irmãos gêmeos retornam à sua cidade natal para recomeçar," +
                    " apenas para descobrir que um mal ainda maior está esperando para recebê-los de volta"
                },
                new Movie
                {
                    Id = 7,
                    Title = "Companion",
                    Genre = MovieGenre.Thriller,
                    Director = "Drew Hancock",
                    ReleaseDate = new DateTime(2025, 1, 31),
                    ImagePath = "ms-appx:///Assets/companion.jpg",
                    Synopsis = "A morte de um bilionário desencadeia uma série de eventos para Iris e seus amigos" +
                    " durante uma viagem de fim de semana à propriedade dele à beira do lago."
                },
                new Movie
                {
                    Id = 8,
                    Title = "Pride and Prejudice",
                    Genre = MovieGenre.Drama,
                    Director = "Joe Wright",
                    ReleaseDate = new DateTime(2005, 9, 16),
                    ImagePath = "ms-appx:///Assets/prideandprejudice.jpg",
                    Synopsis = "Uma história de amor e vida entre a aristocracia inglesa durante a era georgiana." +
                    " Mr. Bennet é um cavalheiro que vive em Hertfordshire com sua esposa autoritária e cinco filhas, mas se ele morrer," +
                    " a casa será herdada por um primo distante que eles nunca conheceram," +
                    " então a felicidade e segurança futura da família dependem de bons casamentos das filhas."
                },
                new Movie
                {
                    Id = 9,
                    Title = "Conclave",
                    Genre = MovieGenre.Thriller,
                    Director = "Edward Berger",
                    ReleaseDate = new DateTime(2024, 8, 30),
                    ImagePath = "ms-appx:///Assets/conclave.jpg",
                    Synopsis = "Após a morte inesperada do Papa, o Cardeal Lawrence é encarregado de gerenciar o ritual secreto e antigo de eleição de um novo Papa." +
                    " Isolado no Vaticano com os líderes mais poderosos da Igreja Católica até que o processo seja concluído," +
                    " Lawrence se encontra no centro de uma conspiração que pode levar à queda da Igreja."
                },
                new Movie
                {
                    Id = 10,
                    Title = "The Amateur",
                    Genre = MovieGenre.Action,
                    Director = "James Hawes",
                    ReleaseDate = new DateTime(2025, 4, 11),
                    ImagePath = "ms-appx:///Assets/maateur.jpg",
                    Synopsis = "Depois que sua vida é virada de cabeça para baixo quando sua esposa é morta em um ataque terrorista em Londres, um brilhante," +
                    " mas introvertido decodificador da CIA decide agir por conta própria quando seus supervisores se recusam a tomar uma atitude."
                },
                new Movie
                {
                    Id = 11,
                    Title = "Mickey 17",
                    Genre = MovieGenre.ScienceFiction,
                    Director = "Bong Joon Ho",
                    ReleaseDate = new DateTime(2025, 3, 7),
                    ImagePath = "ms-appx:///Assets/mickey.jpg",
                    Synopsis = "O improvável herói Mickey Barnes se encontra na extraordinária circunstância de trabalhar para um empregador" +
                    " que exige o compromisso máximo com o trabalho... morrer, para viver."
                },
                new Movie
                {
                    Id = 12,
                    Title = "Warfare",
                    Genre = MovieGenre.Action,
                    Director = "Alex Garland",
                    ReleaseDate = new DateTime(2025, 4, 11),
                    ImagePath = "ms-appx:///Assets/warfare.jpg",
                    Synopsis = "Um pelotão de Navy SEALs americanos em uma missão de vigilância que deu errado em território insurgente." +
                    " Uma história de guerra moderna e irmandade, contada em tempo real e baseada na memória das pessoas que a viveram."
                },
                new Movie
                {
                    Id = 13,
                    Title = "Interstellar",
                    Genre = MovieGenre.ScienceFiction,
                    Director = "Christopher Nolan",
                    ReleaseDate = new DateTime(2014, 11, 7),
                    ImagePath = "ms-appx:///Assets/interstellar.jpg",
                    Synopsis = "As aventuras de um grupo de exploradores que utilizam um recém-descoberto buraco de minhoca para superar" +
                    " as limitações da viagem espacial humana e conquistar as vastas distâncias envolvidas em uma viagem interestelar."
                },
                new Movie
                {
                    Id = 14,
                    Title = "Memento",
                    Genre = MovieGenre.Thriller,
                    Director = "Christopher Nolan",
                    ReleaseDate = new DateTime(2001, 5, 25),
                    ImagePath = "ms-appx:///Assets/memento.jpg",
                    Synopsis = "Leonard Shelby está rastreando o homem que estuprou e assassinou sua esposa." +
                    " A dificuldade de localizar o assassino de sua esposa, no entanto," +
                    " é agravada pelo fato de que ele sofre de uma forma rara e incurável de perda de memória de curto prazo." +
                    " Embora ele consiga lembrar detalhes da vida antes do acidente, Leonard não consegue lembrar" +
                    " o que aconteceu quinze minutos atrás, para onde está indo ou por quê."
                },
                new Movie
                {
                    Id = 15,
                    Title = "The Godfather",
                    Genre = MovieGenre.Crime,
                    Director = "Francis Ford Coppola",
                    ReleaseDate = new DateTime(1972, 3, 24),
                    ImagePath = "ms-appx:///Assets/the-godfather.jpg",
                    Synopsis = "Abrangendo os anos de 1945 a 1955, uma crônica da fictícia família criminosa ítalo-americana Corleone." +
                    " Quando o patriarca da família do crime organizado, Vito Corleone, mal sobrevive a uma tentativa de assassinato," +
                    " seu filho mais novo, Michael, assume a responsabilidade de cuidar dos possíveis assassinos," +
                    " lançando uma campanha de vingança sangrenta."
                },
                new Movie
                {
                    Id = 16,
                    Title = "The Dark Knight",
                    Genre = MovieGenre.Action,
                    Director = "Christopher Nolan",
                    ReleaseDate = new DateTime(2008, 7, 18),
                    ImagePath = "ms-appx:///Assets/batman.jpg",
                    Synopsis = "Batman eleva as apostas em sua guerra contra o crime." +
                    " Com a ajuda do Tenente Jim Gordon e do Promotor Harvey Dent," +
                    " Batman se propõe a desmantelar as organizações criminosas restantes que atormentam as ruas. A parceria se mostra eficaz, " +
                    "mas logo eles se veem vítimas de um reinado de caos desencadeado por um crescente gênio do crime conhecido pelos cidadãos " +
                    "aterrorizados de Gotham como o Coringa."
                },
                new Movie
                {
                    Id = 17,
                    Title = "Forrest Gump",
                    Genre = MovieGenre.Drama,
                    Director = "Robert Zemeckis",
                    ReleaseDate = new DateTime(1994, 7, 6),
                    ImagePath = "ms-appx:///Assets/forrestgump.jpg",
                    Synopsis = "Um homem com baixo QI realizou grandes feitos em sua vida" +
                    " e esteve presente durante eventos históricos significativos—em cada caso," +
                    " superando muito o que qualquer um imaginava que ele poderia fazer. Mas, apesar de tudo o que ele conquistou," +
                    " seu verdadeiro amor lhe escapa."
                },
                new Movie
                {
                    Id = 18,
                    Title = "Se7en",
                    Genre = MovieGenre.Crime,
                    Director = "David Fincher",
                    ReleaseDate = new DateTime(1995, 9, 22),
                    ImagePath = "ms-appx:///Assets/seven.jpg",
                    Synopsis = "Dois detetives de homicídios estão em uma busca desesperada por um serial killer" +
                    " cujos crimes são baseados nos \"sete pecados capitais\" neste filme sombrio e perturbador que " +
                    "leva os espectadores dos restos torturados de uma vítima para a próxima. O experiente Detetive Somerset " +
                    "pesquisa cada pecado na tentativa de entrar na mente do assassino, enquanto seu parceiro novato, Mills," +
                    " zomba de seus esforços para desvendar o caso."
                },
                new Movie
                {
                    Id = 19,
                    Title = "Parasite",
                    Genre = MovieGenre.Drama,
                    Director = "Bong Joon Ho",
                    ReleaseDate = new DateTime(2019, 11, 8),
                    ImagePath = "ms-appx:///Assets/parasite.jpg",
                    Synopsis = "Todos desempregados, a família de Ki-taek tem um interesse peculiar nos ricos " +
                    "e glamorosos Parks para sua subsistência, até que se envolvem em um incidente inesperado."
                },
                new Movie
                {
                    Id = 20,
                    Title = "Whiplash",
                    Genre = MovieGenre.Drama,
                    Director = "Damien Chazelle",
                    ReleaseDate = new DateTime(2014, 10, 15),
                    ImagePath = "ms-appx:///Assets/whiplash.jpg",
                    Synopsis = "Sob a direção de um instrutor implacável," +
                    " um jovem baterista talentoso começa a buscar a perfeição a qualquer custo," +
                    " até mesmo sua humanidade."
                }
            );


        }

   
        public void Initialize()
        {
            Database.EnsureCreated(); 
        }
        
    }
}
using CleanArchitecture.Data;
using CleanArchitecture.Domian;

StreamerDbContext dbContext = new();

void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();
    foreach (var streame in streamers)
    {
        Console.WriteLine($"{streame.Id} - {streame.Nombre}");
    }
}


async Task AddNewRecords()
{


    Streamer streamer = new()
    {
        Nombre = "Amazon Prime",
        Url = "https://www.AmazonPrime.com"
    };

    dbContext!.Streamers!.Add(streamer);
    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video
    {
        Nombre = "Mad Max",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Batman ",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Crepusculo",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Citizen Kane",
        StreamerId = streamer.Id
    }

};

    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();

}






using CleanArchitecture.Data;
using CleanArchitecture.Domian;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();


//await AddNewRecords();
//QueryStreaming();

await QueryFilter();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();


async Task QueryFilter()
{
    Console.WriteLine($"Ingrese cualquier compania de streaming");
    var streamingNombre = Console.ReadLine();

    var streamers = await dbContext!.Streamers!.Where( x => x.Nombre.Equals(streamingNombre)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - { streamer.Nombre }");        
    }

    // var streamersPartialResult = await dbContext!.Streamers!.Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync();

     var streamersPartialResult = await dbContext!.Streamers!.Where(x => EF.Functions.Like( x.Nombre , $"%{streamingNombre}%" ) ).ToListAsync();

    foreach (var streamer in streamersPartialResult)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }



}

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
        Nombre = "Disney",
        Url = "https://www.Disney.com"
    };

    dbContext!.Streamers!.Add(streamer);
    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video
    {
        Nombre = "La Cenicientas",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "101 Dalmatas ",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "El jorobado de Notredame",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Star Wars",
        StreamerId = streamer.Id
    }

};

    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();

}






using Ardaite.Presentation.AppBuilder;

const string view = """
                    (window title="A window"
                        (label text="Hello, world!" color="red" size="30"))
                    """;

ArdaiteAppBuilder
    .CreateBuilder()
    .LoadFont("Roboto.ttf")
    .UseMarkup(view)
    .Build()
    .Run();
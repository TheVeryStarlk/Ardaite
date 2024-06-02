# Ardaite
A toy GUI "framework" project that has a markup language with a simple presentation layer using SFML.

## Usage
```cs
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
```

<p align="center">
  <img align="center" src="https://i.imgur.com/hKbAmbK.png">
</p>
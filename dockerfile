# Utiliza a imagem do SDK .NET 8 como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia csproj e restaura as dependências
COPY ./*.sln ./
COPY ./App.Desafio.Blog.Api/*.csproj ./App.Desafio.Blog.Api/
COPY ./App.Desafio.Blog.Domain/*.csproj ./App.Desafio.Blog.Domain/
COPY ./App.Desafio.Blog.Application/*.csproj ./App.Desafio.Blog.Application/
COPY ./App.Desafio.Blog.Infra.Data/*.csproj ./App.Desafio.Blog.Infra.Data/
COPY ./App.Desafio.Blog.Crosscutting/*.csproj ./App.Desafio.Blog.Crosscutting/
RUN dotnet restore ./App.Desafio.Blog.Api/App.Desafio.Blog.Api.csproj

# Copia os arquivos do projeto e compila
COPY . ./
RUN dotnet publish ./App.Desafio.Blog.Api/App.Desafio.Blog.Api.csproj -c Release -o out

# Listar os conteúdos antes da publicação para diagnóstico
RUN ls -la ./App.Desafio.Blog.Api/
RUN ls -la ./App.Desafio.Blog.Domain/
RUN ls -la ./App.Desafio.Blog.Application/
RUN ls -la ./App.Desafio.Blog.Infra.Data/
RUN ls -la ./App.Desafio.Blog.Crosscutting/

# Listar diretório de saída para verificar a existência do arquivo DLL
RUN ls -la out/

# Gera a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "App.Desafio.Blog.Api.dll"]
